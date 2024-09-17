// Login.js
import React, { useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import s from './login.module.css'
import {NavLink} from "react-router-dom";
import {AuthAPI} from "../../../api/api";
import { redirect } from "react-router-dom";
import { useNavigate } from 'react-router';


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

const Login = (props) => {
    const classes = useStyles();
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const navigate = useNavigate();

    const [err, setErr] = useState('');

    const handleSubmit = async (event) =>{
        event.preventDefault();

        try {
            setErr("")
            const response = await AuthAPI.Login(username, password);
            console.log(response)
            try {
                const user = await AuthAPI.Auth();
                let userData = user.data
                console.log(userData)
                let isAdmin = userData.isAdmin ? true : false
                props.setUser({ isAuth: true, id: 0, email: username, isAdmin: userData.isAdmin})

                navigate("/homepage");
            } 
            catch (error) {
                if (error.response) {
                    setErr(error.response.data.message)
                }
                props.setUser({ isAuth: false, id: 0, email: "", isAdmin: false})
            }
        } catch (error) {
            if (error.response) {
                setErr(error.response.data.message)
            }
            props.setUser({ isAuth: false, id: 0, email: "", isAdmin: false})
        }
    };

    return (
        <div className={s.container}>
            <h2 className={s.LoginLabel}>Вход</h2>
            <form onSubmit={handleSubmit}>
                <div className={classes.formGroup}>
                    <TextField
                        label="Email"
                        variant="outlined"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
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
                <Button
                    type="submit"
                    variant="contained"
                    color="primary"
                    className={classes.submitButton}
                >
                    Login
                </Button>
            </form>
            <div className={s.err}>{err}</div>
            <NavLink to={"/registration"} className={s.LoginLabel}>Нет аккаунта</NavLink>


        </div>
    );
};

export default Login;