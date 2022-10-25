import { createRef, useState } from "react";
import { Navigate } from "react-router-dom";
import { container } from "../../ioc";
import { IAuthenticationService } from "../../services/authentication/authentication.service";

function Login() {
    const authenticationService = container.get<IAuthenticationService>("authentication-service");
    const loginForm: React.RefObject<HTMLFormElement> = createRef<HTMLFormElement>();

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [formSubmitted, setFormSubmitted] = useState(false);
    const [loginFailed, setLoginFailed] = useState(false);
    const [loginSuccessful, setLoginSuccessful] = useState(false);

    const emailChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
        setEmail(event.target.value);
    };

    const passwordChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
        setPassword(event.target.value);
    }

    const performLogin = async (event: React.ChangeEvent<HTMLFormElement>) => {
        event.preventDefault();

        setFormSubmitted(true);

        if (loginForm.current && loginForm.current.checkValidity()) {
            try {
                await authenticationService.login(email, password);

                setLoginFailed(false);
                setLoginSuccessful(true);
            } catch {
                setLoginFailed(true);
                setLoginSuccessful(false);
            }
        }
    };

    return (
        loginSuccessful ?
            <Navigate to="/" /> :
            <main>
                <h1>Login</h1>
                <div className={`alert alert-danger ${!loginFailed ? 'd-none' : ''}`}>
                    Your user name and/or password were incorrect. Please try again.
                </div>
                <form id="login-form" ref={loginForm} className={`needs-validation ${formSubmitted ? 'was-validated' : ''}`} noValidate onSubmit={performLogin}>
                    <div className="container-fluid p-0">
                        <div className="row form-group">
                            <div className="col-lg-2 col-sm-3 col-12">
                                <label htmlFor="email-address" className="col-form-label">Email address:</label>
                            </div>
                            <div className="col-lg-10 col-sm-9 col-12">
                                <input id="email-address" type="email" className="form-control" required value={email} onChange={emailChanged} />
                                <div className="invalid-feedback">
                                    Please provide an email address.
                                </div>
                            </div>
                        </div>
                        <div className="row form-group">
                            <div className="col-lg-2 col-sm-3 col-12">
                                <label htmlFor="password" className="col-form-label">Password:</label>
                            </div>
                            <div className="col-lg-10 col-sm-9 col-12">
                                <input id="password" type="password" className="form-control" required value={password} onChange={passwordChanged} />
                                <div className="invalid-feedback">
                                    Please provide a password.
                                </div>
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-lg-10 col-sm-9 col-12 offset-lg-2 offset-sm-3">
                                <button type="submit" className="btn btn-primary">Login</button>
                            </div>
                        </div>
                    </div>
                </form>
            </main>);
}

export default Login;