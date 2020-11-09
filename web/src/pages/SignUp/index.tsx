import React, { ChangeEvent, FormEvent, useCallback, useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import {
  Alert,
  Button,
  Container,
  Form,
  FormGroup,
  Input,
  Label,
} from 'reactstrap';

import { IUserSignUp } from '../../@types/entities';
import api from '../../services/api';
import './styles.css';

const SignUp: React.FC = () => {
  const [user, setUser] = useState<IUserSignUp>({
    name: '',
    email: '',
    phoneNumber: '',
    role: 1,
  });
  const [alertError, setAlertError] = useState('');

  const history = useHistory();

  const handleInputChange = useCallback(
    (event: ChangeEvent<HTMLInputElement>) => {
      const { name, value } = event.currentTarget;

      setUser({ ...user, [name]: value });
    },
    [user],
  );

  const handleSubmitSignUp = useCallback(
    async (event: FormEvent<HTMLFormElement>) => {
      event.preventDefault();

      try {
        console.log(user);
        await api.post('/users', user);

        alert('Cadastro efetuado com sucesso!');

        setAlertError('');
        history.push('/');
      } catch (error) {
        setAlertError(
          'Falha ao fazer o cadastro. Verifique os dados e tente novamente.',
        );
        console.log(error);
      }
    },
    [history, user],
  );

  return (
    <Container className="background-container">
      <Container className="signup-container">
        <h3>Bem vindo ao Easy Interests!</h3>
        <p>Faça seu cadastro em nossa plataforma</p>

        {alertError.length > 0 && <Alert color="danger">{alertError}</Alert>}
        <Form onSubmit={handleSubmitSignUp}>
          <FormGroup>
            <Label name="name">Nome</Label>
            <Input
              className="form-control"
              name="name"
              value={user.name}
              onChange={handleInputChange}
            />
          </FormGroup>

          <FormGroup>
            <Label name="email">E-mail</Label>
            <Input
              className="form-control"
              name="email"
              type="email"
              value={user.email}
              onChange={handleInputChange}
            />
          </FormGroup>

          <FormGroup>
            <Label name="phoneNumber">Telefone (ou celular, se preferir)</Label>
            <Input
              className="form-control"
              name="phoneNumber"
              value={user.phoneNumber}
              onChange={handleInputChange}
            />
          </FormGroup>

          <FormGroup>
            <Label htmlFor="role">Função</Label>
            <Input
              type="select"
              name="role"
              value={user.role}
              onChange={handleInputChange}
            >
              <option value={1}>Cliente</option>
              <option value={2}>Negociador</option>
            </Input>
          </FormGroup>

          <Button type="submit" color="primary">
            Cadastrar-se
          </Button>
        </Form>
        <Link to="/">
          <p>Voltar para login</p>
        </Link>
      </Container>
    </Container>
  );
};

export default SignUp;
