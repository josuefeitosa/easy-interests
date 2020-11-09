/* eslint-disable react/jsx-curly-newline */
/* eslint-disable no-param-reassign */
/* eslint-disable no-nested-ternary */
import React, { useState } from 'react';
import {
  Alert,
  Badge,
  Button,
  Col,
  Form,
  FormGroup,
  Input,
  InputGroup,
  InputGroupAddon,
  Label,
  Row,
} from 'reactstrap';
import moment from 'moment';
import { Field, FieldArray, Formik } from 'formik';
import { IDebtList, INewDebt, IUser } from '../@types/entities';
import api from '../services/api';

interface IDebtFormProps {
  debt: IDebtList | INewDebt;
  isEditing: boolean;
  modalClose(): void;
  submitFunction(debt: IDebtList | INewDebt, isUpdating: boolean): void;
}

const DebtForm: React.FC<IDebtFormProps> = ({
  debt,
  isEditing,
  modalClose,
  submitFunction,
}) => {
  // console.log(debt);

  const [isUpdating] = useState(() => debt.description.length > 0);
  const [customers, setCustomers] = useState<IUser[]>(() => {
    if (isEditing) {
      const token = JSON.parse(
        JSON.stringify(localStorage.getItem('@easyInterests:token')),
      );

      api
        .get<IUser[]>('/users', {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          const filteredCustomers = response.data.filter(
            (user) => user.role === 'Customer',
          );

          filteredCustomers.sort((a, b) => (a.name > b.name ? 1 : -1));

          setCustomers(filteredCustomers);
        });
    }
    return [] as IUser[];
  });
  const [alertError, setAlertError] = useState('');

  return customers ? (
    <>
      {alertError.length > 0 && <Alert color="danger">{alertError}</Alert>}
      <Formik
        initialValues={debt}
        onSubmit={(values, { setSubmitting }) => {
          if (!isEditing) modalClose();

          if (moment(Date.now()).isBefore(values.dueDate))
            setAlertError('Data de vencimento deve ser menor que hoje!');
          else if (
            moment(Date.now()).diff(values.dueDate, 'month') < 1 &&
            values.interestInterval.toString() === '2'
          )
            setAlertError(
              'Diferença entre hoje e vencimento da dívida é menor que um mês!',
            );
          else if (
            moment(Date.now()).diff(values.dueDate, 'year') < 1 &&
            values.interestInterval.toString() === '3'
          )
            setAlertError(
              'Diferença entre hoje e vencimento da dívida é menor que um ano!',
            );
          else {
            if (!isUpdating) {
              values.negotiatorComissionPercentage = parseFloat(
                (values.negotiatorComissionPercentage / 100).toFixed(4),
              );
              values.interestPercentage = parseFloat(
                (values.interestPercentage / 100).toFixed(4),
              );
            }

            values.dueDate = moment(values.dueDate).format(
              'yyyy-MM-DDTHH:mm:ssZ',
            );

            setAlertError('');
            submitFunction(values, isUpdating);
            setSubmitting(false);
            modalClose();
          }
        }}
      >
        {(formikProps) => {
          const {
            values,
            handleSubmit,
            handleChange,
            handleBlur,
            setFieldValue,
          } = formikProps;

          if (isEditing) {
            return (
              <Form onSubmit={handleSubmit}>
                <Row>
                  <Col lg="4">
                    <FormGroup>
                      <Label htmlFor="customerId">Cliente</Label>
                      {!isUpdating ? (
                        <Input
                          type="select"
                          name="customerId"
                          onChange={(e) =>
                            setFieldValue('customerId', e.currentTarget.value)
                          }
                          disabled={isUpdating}
                          required={!isUpdating}
                        >
                          <option value="">Selecione um cliente...</option>
                          {customers.map((customer) => (
                            <option key={customer.id} value={customer.id}>
                              {customer.name}
                            </option>
                          ))}
                        </Input>
                      ) : (
                        <Input tag={Field} readOnly name="customerName" />
                      )}
                    </FormGroup>
                  </Col>
                  <Col lg="4">
                    <FormGroup>
                      <Label htmlFor="description">Descrição</Label>
                      <Input
                        tag={Field}
                        name="description"
                        readOnly={isUpdating}
                        required={!isUpdating}
                      />
                    </FormGroup>
                  </Col>
                  {isUpdating && (
                    <Col lg="3">
                      <FormGroup>
                        <p>Status</p>
                        <Badge
                          color={
                            (values as IDebtList).paid ? 'success' : 'danger'
                          }
                        >
                          {(values as IDebtList).paid ? 'Quitada' : 'Em aberto'}
                        </Badge>
                      </FormGroup>
                    </Col>
                  )}
                  <Col lg="4">
                    <FormGroup>
                      <Label htmlFor="dueDate">Data de Vencto.</Label>
                      <Input
                        type={isUpdating ? 'text' : 'date'}
                        required={!isUpdating}
                        name="dueDate"
                        readOnly={isUpdating}
                        value={
                          isUpdating
                            ? moment(values.dueDate).format('DD/MM/YYYY')
                            : values.dueDate
                        }
                        onChange={(e) =>
                          setFieldValue('dueDate', e.currentTarget.value)
                        }
                      />
                    </FormGroup>
                  </Col>
                </Row>
                <Row>
                  <Col lg="4">
                    <FormGroup>
                      <Label htmlFor="originalValue">Valor Original</Label>
                      <InputGroup>
                        <InputGroupAddon addonType="prepend">
                          R$
                        </InputGroupAddon>
                        <Input
                          required={!isUpdating}
                          readOnly={isUpdating}
                          tag={Field}
                          name="originalValue"
                          type="number"
                        />
                      </InputGroup>
                    </FormGroup>
                  </Col>
                  <Col lg="4">
                    <Label htmlFor="parcelsQty">Qtd. Parcelas</Label>
                    <Input
                      tag={Field}
                      type="number"
                      name="parcelsQty"
                      readOnly={isUpdating}
                      value={values.parcelsQty}
                    />
                  </Col>
                  <Col lg="4">
                    <Label htmlFor="negotiatorComissionPercentage">
                      % Comissão
                    </Label>
                    <InputGroup>
                      <Input
                        tag={Field}
                        type="number"
                        name="negotiatorComissionPercentage"
                        required
                        readOnly={isUpdating}
                        value={
                          isUpdating
                            ? (
                                values.negotiatorComissionPercentage * 100
                              ).toFixed(2)
                            : values.negotiatorComissionPercentage
                        }
                      />
                      <InputGroupAddon addonType="append">%</InputGroupAddon>
                    </InputGroup>
                  </Col>
                </Row>
                <Row>
                  <Col lg="4">
                    <Label htmlFor="interestPercentage">% Juros</Label>
                    <InputGroup>
                      <Input
                        tag={Field}
                        type="number"
                        name="interestPercentage"
                        required
                        readOnly={isUpdating}
                        value={
                          isUpdating
                            ? (values.interestPercentage * 100).toFixed(2)
                            : values.interestPercentage
                        }
                      />
                      <InputGroupAddon addonType="append">%</InputGroupAddon>
                    </InputGroup>
                  </Col>
                  <Col lg="4">
                    <Label htmlFor="interestInterval">Intervalo de Juros</Label>
                    <Input
                      type="select"
                      name="interestInterval"
                      value={values.interestInterval}
                      onChange={(e) =>
                        setFieldValue('interestInterval', e.currentTarget.value)
                      }
                      onBlur={handleBlur}
                      disabled={isUpdating}
                      required
                    >
                      <option value="">Selecione um intervalo...</option>
                      <option value={1}>ao Dia</option>
                      <option value={2}>ao Mês</option>
                      <option value={3}>ao Ano</option>
                    </Input>
                  </Col>
                  <Col lg="4">
                    <Label htmlFor="interestType">Tipo de Juros</Label>
                    <Input
                      type="select"
                      name="interestType"
                      value={values.interestType}
                      onChange={handleChange}
                      onBlur={handleBlur}
                      disabled={isUpdating}
                      required
                    >
                      <option value="">Selecione um tipo...</option>
                      <option value={1}>Simples</option>
                      <option value={2}>Composto</option>
                    </Input>
                  </Col>
                </Row>

                {isUpdating && (
                  <Row className="mt-2">
                    <Col lg="12">
                      <h5>Parcelas</h5>
                      <div className="p-3 border rounded">
                        <FieldArray name="parcels">
                          {() => {
                            return (values as IDebtList).parcels.map(
                              (parcel, index) => (
                                <Row key={parcel.parcel}>
                                  <Col lg="2">
                                    <FormGroup>
                                      <Label
                                        htmlFor={`parcels.${index}.parcel`}
                                      >
                                        Parcela
                                      </Label>
                                      <Input
                                        type="number"
                                        readOnly
                                        value={parcel.parcel}
                                      />
                                    </FormGroup>
                                  </Col>
                                  <Col lg="4">
                                    <FormGroup>
                                      <Label
                                        htmlFor={`parcels.${index}.dueDate`}
                                      >
                                        Parcela
                                      </Label>
                                      <Input
                                        type="text"
                                        readOnly
                                        value={moment(parcel.dueDate).format(
                                          'DD/MM/YYYY',
                                        )}
                                      />
                                    </FormGroup>
                                  </Col>
                                  <Col lg="3">
                                    <FormGroup>
                                      <Label htmlFor={`parcels.${index}.value`}>
                                        Parcela
                                      </Label>
                                      {parcel.value && (
                                        <Input
                                          type="text"
                                          readOnly
                                          value={Intl.NumberFormat('pt-BR', {
                                            style: 'currency',
                                            currency: 'BRL',
                                          }).format(parcel.value)}
                                        />
                                      )}
                                    </FormGroup>
                                  </Col>
                                  <Col lg="3">
                                    <FormGroup>
                                      <Label htmlFor={`parcels.${index}.paid`}>
                                        Status
                                      </Label>
                                      <Input
                                        type="select"
                                        name={`parcels.${index}.paid`}
                                        onChange={(e) =>
                                          setFieldValue(
                                            `parcels.${index}.paid`,
                                            e.currentTarget.value === 'Pago',
                                          )
                                        }
                                        value={
                                          parcel.paid ? 'Pago' : 'Em aberto'
                                        }
                                      >
                                        <option value="Pago">Quitado</option>
                                        <option value="Em aberto">
                                          Em aberto
                                        </option>
                                      </Input>
                                    </FormGroup>
                                  </Col>
                                </Row>
                              ),
                            );
                          }}
                        </FieldArray>
                      </div>
                    </Col>
                  </Row>
                )}

                <Row className="mt-4">
                  <Col lg="6">
                    <Button type="submit" color="success" block>
                      Salvar
                    </Button>
                  </Col>
                  <Col lg="6">
                    <Button
                      type="button"
                      color="danger"
                      block
                      onClick={modalClose}
                    >
                      Fechar
                    </Button>
                  </Col>
                </Row>
              </Form>
            );
          }

          return (
            <Form onSubmit={handleSubmit}>
              <Row>
                <Col lg="4">
                  <FormGroup>
                    <Label htmlFor="customerName">Cliente</Label>
                    <Input tag={Field} name="customerName" readOnly />
                  </FormGroup>
                </Col>
                <Col lg="5">
                  <FormGroup>
                    <Label htmlFor="description">Descrição</Label>
                    <Input tag={Field} name="description" readOnly />
                  </FormGroup>
                </Col>
                <Col lg="3">
                  <FormGroup>
                    <p>Status</p>
                    <Badge
                      color={(values as IDebtList).paid ? 'success' : 'danger'}
                    >
                      {(values as IDebtList).paid ? 'Quitada' : 'Em aberto'}
                    </Badge>
                  </FormGroup>
                </Col>
              </Row>
              <Row>
                <Col lg="4">
                  <FormGroup>
                    <Label htmlFor="originalValue">Valor Original</Label>
                    <Input
                      type="text"
                      readOnly
                      value={Intl.NumberFormat('pt-BR', {
                        style: 'currency',
                        currency: 'BRL',
                      }).format(values.originalValue)}
                    />
                  </FormGroup>
                </Col>
                <Col lg="4">
                  <FormGroup>
                    <Label htmlFor="dueDate">Vencto. Original</Label>
                    <Input
                      type="text"
                      readOnly
                      value={moment(values.dueDate).format('DD/MM/YYYY')}
                    />
                  </FormGroup>
                </Col>
                <Col lg="4">
                  <Label htmlFor="parcelQty">Qtd. Parcelas</Label>
                  <Input
                    type="text"
                    name="parcelQty"
                    readOnly
                    value={values.parcelsQty}
                  />
                </Col>
              </Row>
              <Row>
                <Col lg="4">
                  <FormGroup>
                    <Label htmlFor="negotiatorName">Nome do Negociador</Label>
                    <Input
                      type="text"
                      name="negotiatorName"
                      readOnly
                      value={(values as IDebtList).negotiatorName}
                    />
                  </FormGroup>
                </Col>
                <Col lg="4">
                  <FormGroup>
                    <Label htmlFor="calculationDate">Data de Cálculo</Label>
                    <Input
                      type="text"
                      readOnly
                      value={moment(
                        (values as IDebtList).calculationDate,
                      ).format('DD/MM/YYYY')}
                    />
                  </FormGroup>
                </Col>
                <Col lg="4">
                  <FormGroup>
                    <Label htmlFor="interest">Juros</Label>
                    <Input
                      type="text"
                      readOnly
                      value={`${(
                        (debt as IDebtList).interestPercentage * 100
                      ).toFixed(2)}% ao ${
                        (debt as IDebtList).interestType === 3
                          ? 'ano'
                          : (debt as IDebtList).interestType === 2
                          ? 'mês'
                          : 'dia'
                      }`}
                    />
                  </FormGroup>
                </Col>
              </Row>

              <Row className="mt-2">
                <Col lg="12">
                  <h5>Parcelas</h5>
                  <div className="p-3 border rounded">
                    <FieldArray name="parcels">
                      {() => {
                        return (values as IDebtList).parcels.map(
                          (parcel, index) => (
                            <Row key={parcel.parcel}>
                              <Col lg="2">
                                <FormGroup>
                                  <Label htmlFor={`parcels.${index}.parcel`}>
                                    Parcela
                                  </Label>
                                  <Input
                                    type="number"
                                    readOnly
                                    value={parcel.parcel}
                                  />
                                </FormGroup>
                              </Col>
                              <Col lg="4">
                                <FormGroup>
                                  <Label htmlFor={`parcels.${index}.dueDate`}>
                                    Parcela
                                  </Label>
                                  <Input
                                    type="text"
                                    readOnly
                                    value={moment(parcel.dueDate).format(
                                      'DD/MM/YYYY',
                                    )}
                                  />
                                </FormGroup>
                              </Col>
                              <Col lg="3">
                                <FormGroup>
                                  <Label htmlFor={`parcels.${index}.value`}>
                                    Parcela
                                  </Label>
                                  {parcel.value && (
                                    <Input
                                      type="text"
                                      readOnly
                                      value={Intl.NumberFormat('pt-BR', {
                                        style: 'currency',
                                        currency: 'BRL',
                                      }).format(parcel.value)}
                                    />
                                  )}
                                </FormGroup>
                              </Col>
                              <Col lg="3">
                                <FormGroup>
                                  <p>Status</p>
                                  <Badge
                                    color={parcel.paid ? 'success' : 'danger'}
                                  >
                                    {parcel.paid ? 'Quitada' : 'Em aberto'}
                                  </Badge>
                                </FormGroup>
                              </Col>
                            </Row>
                          ),
                        );
                      }}
                    </FieldArray>
                  </div>
                </Col>
              </Row>

              <Row className="mt-2">
                <Col lg="6">
                  <FormGroup>
                    <Label htmlFor="">
                      Telefone para contato do negociador
                    </Label>
                    <Input
                      type="text"
                      readOnly
                      value={(values as IDebtList).negotiatorPhone}
                    />
                  </FormGroup>
                </Col>
              </Row>

              <Button type="submit" color="danger" block>
                Fechar
              </Button>
            </Form>
          );
        }}
      </Formik>
    </>
  ) : (
    <div>Carregando dados...</div>
  );
};

export default DebtForm;
