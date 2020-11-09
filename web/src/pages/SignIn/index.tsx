import React, { FormEvent, useCallback, useState } from 'react';
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

import { useAuth } from '../../hooks/auth';
import './styles.css';

const SignIn: React.FC = () => {
  const [emailLogin, setEmailLogin] = useState('');
  const [alertError, setAlertError] = useState('');

  const { signIn } = useAuth();
  const history = useHistory();

  const handleSignIn = useCallback(
    async (event: FormEvent<HTMLFormElement>) => {
      event.preventDefault();

      try {
        await signIn({ email: emailLogin });

        setAlertError('');
        history.push('/dashboard');
      } catch (error) {
        setAlertError(
          'Falha ao realizar login. Verifique seu e-mail e tente novamente.',
        );
        console.log(error);
      }
    },
    [emailLogin, history, signIn],
  );

  return (
    <Container className="background-container">
      <Container className="login-container">
        <h3>Bem vindo ao Easy Interests!</h3>
        <p>Digite seu email para entrar na plataforma</p>

        {alertError.length > 0 && <Alert color="danger">{alertError}</Alert>}
        <Form onSubmit={handleSignIn}>
          <FormGroup>
            <Label htmlFor="email">E-mail</Label>
            <Input
              className="form-control"
              name="email"
              value={emailLogin}
              onChange={(e) => {
                setEmailLogin(e.currentTarget.value);
              }}
            />
          </FormGroup>

          <Button type="submit" color="primary">
            Login
          </Button>
        </Form>
        <Link to="/signup">
          <p>Faça seu cadastro</p>
        </Link>
      </Container>
    </Container>
  );
};

export default SignIn;
