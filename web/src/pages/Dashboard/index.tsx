/* eslint-disable no-param-reassign */
/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable no-nested-ternary */
import React, { useCallback, useEffect, useState } from 'react';
import moment from 'moment';
import {
  Badge,
  Button,
  Container,
  Modal,
  ModalBody,
  ModalHeader,
  Table,
} from 'reactstrap';
import { FiLogOut, FiEdit, FiEye } from 'react-icons/fi';

import './styles.css';
import { useHistory } from 'react-router-dom';
import {
  IDebtList,
  IDebtUpdate,
  IDebtUpdateParcel,
  INewDebt,
} from '../../@types/entities';
import api from '../../services/api';
import DebtForm from '../../components/DebtForm';

const Dashboard: React.FC = () => {
  const [signedUser] = useState(() => {
    const storedSignedUser = localStorage.getItem('@easyInterests:user');

    if (storedSignedUser) {
      console.log(JSON.parse(storedSignedUser));

      return JSON.parse(storedSignedUser);
    }
    return {
      name: ' ',
    };
  });
  const [debts, setDebts] = useState<IDebtList[]>();
  const [editing, setEditing] = useState(false);
  const [formModal, setFormModal] = useState(false);

  const blankNewDebt: INewDebt = {
    customerId: 0,
    description: '',
    dueDate: new Date().toString(),
    interestInterval: 0,
    interestPercentage: 0,
    interestType: 0,
    negotiatorComissionPercentage: 0,
    originalValue: 0,
    parcelsQty: 1,
  };

  const [currentDebt, setCurrentDebt] = useState<IDebtList | INewDebt>(
    blankNewDebt,
  );

  useEffect(() => {
    const token = JSON.parse(
      JSON.stringify(localStorage.getItem('@easyInterests:token')),
    );

    api
      .get<IDebtList[]>(`/debt/byuser?userId=${signedUser.id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        console.log(response.data);
        setDebts(response.data);
      });
  }, [signedUser]);

  const history = useHistory();

  const handleLogout = useCallback(async () => {
    await history.push('/');
    localStorage.removeItem('@easyInterests:token');
    localStorage.removeItem('@easyInterests:user');
  }, [history]);

  const handleEdit = useCallback((debt: IDebtList) => {
    setEditing(true);
    setCurrentDebt(debt);
    setFormModal(true);
  }, []);

  const handleCreate = useCallback(() => {
    setEditing(true);
    setCurrentDebt(blankNewDebt);
    setFormModal(true);
  }, [blankNewDebt]);

  const handleVisualize = useCallback((debt: IDebtList) => {
    setEditing(false);
    setCurrentDebt(debt);
    setFormModal(true);
  }, []);

  const handleSendDebt = useCallback(
    async (debt: IDebtList | INewDebt, isUpdating: boolean) => {
      // console.log(isUpdating);

      const token = JSON.parse(
        JSON.stringify(localStorage.getItem('@easyInterests:token')),
      );

      try {
        if (isUpdating) {
          const updatedParcels: IDebtUpdateParcel[] = (debt as IDebtList).parcels.map(
            (parcel) => {
              return {
                parcel: parcel.parcel,
                paid: parcel.paid,
              };
            },
          );

          const updatedDebt: IDebtUpdate = {
            id: (debt as IDebtList).id,
            customerName: (debt as IDebtList).customerName,
            description: (debt as IDebtList).description,
            negotiatorComissionPercentage: (debt as IDebtList)
              .negotiatorComissionPercentage,
            negotiatorName: (debt as IDebtList).negotiatorName,
            negotiatorPhone: (debt as IDebtList).negotiatorPhone,
            paid: (debt as IDebtList).paid,
            parcels: updatedParcels,
          };

          // console.log(updatedDebt);
          await api.put(`/debt?Id=${updatedDebt.id}`, updatedDebt, {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          });
        } else {
          (debt as INewDebt).customerId = parseInt(
            (debt as INewDebt).customerId.toString(),
          );
          debt.interestInterval = parseInt(debt.interestInterval.toString());
          debt.interestType = parseInt(debt.interestType.toString());
          console.log(JSON.stringify(debt));

          await api.post('/debt', debt, {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          });
        }
        alert('Operação realizada com sucesso!');
        window.location.reload();
      } catch (error) {
        console.log(error);
        alert('Falha ao fazer esta operação. Tente novamente.');
      }
    },
    [],
  );

  const toggleModal = useCallback(() => {
    setFormModal(!formModal);
  }, [formModal]);

  const modalClose = useCallback(() => {
    setFormModal(false);
  }, []);

  return (
    <>
      {formModal && (
        <Modal isOpen={formModal} toggle={toggleModal} size="lg">
          <ModalHeader>
            {editing ? 'Gerenciar' : 'Visualizar'} Dívida
          </ModalHeader>
          <ModalBody>
            <DebtForm
              debt={currentDebt}
              isEditing={editing}
              modalClose={modalClose}
              submitFunction={handleSendDebt}
            />
          </ModalBody>
        </Modal>
      )}
      <Container fluid className="dashboard-container">
        <header className="dashboard-header">
          <div className="dashboard-title">Easy Interests</div>

          <div>
            {signedUser.role === 2 && (
              <Button type="button" color="success" onClick={handleCreate}>
                Cadastrar nova dívida.
              </Button>
            )}
          </div>

          <div className="dashboard-title-logout">
            <p>{`Olá, ${signedUser.name}`}</p>
            <a className="ml-1" href="/" onClick={handleLogout}>
              Sair
              <FiLogOut className="ml-2" />
            </a>
          </div>
        </header>

        <Container className="mt-4 dashboard-table-container">
          {debts ? (
            <Table striped responsive size="large">
              <thead>
                <tr>
                  {signedUser.role === 2 && (
                    <th className="text-center">Cliente</th>
                  )}
                  <th className="text-center">Descrição</th>
                  <th className="text-center">Vencimento Original</th>
                  <th className="text-center">Valor Original</th>
                  <th className="text-center">Juros</th>
                  {signedUser.role === 2 && (
                    <th className="text-center">% Comissão</th>
                  )}
                  <th className="text-center">Valor Atualizado</th>
                  <th className="text-center">Qtd. Parcelas</th>
                  <th className="text-center">Status</th>
                  <th className="text-center"> </th>
                </tr>
              </thead>
              <tbody>
                {debts.length > 0 ? (
                  debts.map((debt) => (
                    <tr key={debt.id}>
                      {signedUser.role === 2 && <td>{debt.customerName}</td>}
                      <td>{debt.description}</td>
                      <td className="text-center">
                        {moment(debt.dueDate).format('DD/MM/YYYY')}
                      </td>
                      <td className="text-center">
                        {Intl.NumberFormat('pt-BR', {
                          style: 'currency',
                          currency: 'BRL',
                        }).format(debt.originalValue)}
                      </td>
                      <td>
                        {`${(debt.interestPercentage * 100).toFixed(2)}% ao ${
                          debt.interestType === 3
                            ? 'ano'
                            : debt.interestType === 2
                            ? 'mês'
                            : 'dia'
                        }`}
                      </td>
                      {signedUser.role === 2 && (
                        <td className="text-center">
                          {(debt.negotiatorComissionPercentage * 100).toFixed(
                            2,
                          )}
                          %
                        </td>
                      )}
                      <td className="text-center">
                        {Intl.NumberFormat('pt-BR', {
                          style: 'currency',
                          currency: 'BRL',
                        }).format(debt.recalculatedValue)}
                      </td>
                      <td className="text-center">{debt.parcelsQty}</td>
                      <td className="text-center">
                        <Badge color={debt.paid ? 'success' : 'danger'}>
                          {debt.paid ? 'Quitada' : 'Em aberto'}
                        </Badge>
                      </td>
                      <td>
                        {signedUser.role === 2 ? (
                          <Button
                            type="button"
                            color="primary"
                            onClick={() => {
                              console.log(debt);
                              handleEdit(debt);
                            }}
                          >
                            <FiEdit />
                          </Button>
                        ) : (
                          <Button
                            type="button"
                            color="primary"
                            onClick={() => {
                              handleVisualize(debt);
                            }}
                          >
                            <FiEye />
                          </Button>
                        )}
                      </td>
                    </tr>
                  ))
                ) : (
                  <tr>
                    <td colSpan={signedUser.role === 2 ? 10 : 8}>
                      Não existem dívidas cadastradas.
                    </td>
                  </tr>
                )}
              </tbody>
            </Table>
          ) : (
            'loading'
          )}
        </Container>
      </Container>
    </>
  );
};

export default Dashboard;
