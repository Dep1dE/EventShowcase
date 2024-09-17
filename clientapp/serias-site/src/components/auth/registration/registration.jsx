// Login.js
import React, { useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import s from './registration.module.css'
import {AuthAPI} from "../../../api/api";
import {useNavigate} from "react-router";

const useStyles = makeStyles((theme) => ({
    container: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    formGroup: {
        margin: theme.spacing(1),
    },
    submitButton: {
        margin: theme.spacing(3, 0, 2),
    },
}));

const Registration = (props) => {
    const classes = useStyles();
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [password2, setPassword2] = useState('');

    const [err, setErr] = useState('');

    let navigate = useNavigate()

    const handleSubmit = async (event) => {
        event.preventDefault();

        if (password != password2) {
            setErr("Пароли не совпадают")
            return
        }

        try {
            const data = await AuthAPI.Registration(username, email, password);
            navigate("/login");
        } catch (error) {
            if (error.response) {
                setErr("Поле не может быть пустым")
            }
        }
    };

    return (
        <div className={s.container}>
            <h2 className={s.LoginLabel}>Регистрация</h2>
            <form onSubmit={handleSubmit}>
                <div className={classes.formGroup}>
                    <TextField
                        label="Name"
                        variant="outlined"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                    />
                </div>
                <div className={classes.formGroup}>
                    <TextField
                        label="Email"
                        variant="outlined"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </div>
                <div className={classes.formGroup}>
                    <TextField
                        label="Password"
                        variant="outlined"
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </div>
                <div className={classes.formGroup}>
                    <TextField
                        label="Confirm password"
                        variant="outlined"
                        type="password"
                        value={password2}
                        onChange={(e) => setPassword2(e.target.value)}
                    />
                </div>
                <Button
                    type="submit"
                    variant="contained"
                    color="primary"
                    className={classes.submitButton}
                >
                    Registration
                </Button>
            </form>
            <div className={s.err}>{err}</div>
        </div>
    );
};

export default Registration;