// Login.js
import React, { useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import s from './addImage.module.css'
import {EventAPI} from "../../../api/api";
import dayjs from 'dayjs';
import {useNavigate, useParams} from "react-router";


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

const AddImage = (props) => {
    const { id } = useParams();
    const classes = useStyles();
    const [Link, setLink] = useState('');
    

    

    const [err, setErr] = useState('');

    let navigate = useNavigate()

    const handleSubmit = async (event) => {
        event.preventDefault();

        
        try {
            console.log(id)
            const data = await EventAPI.AddImage(id,Link);
            navigate("/events");
        } catch (error) {
            if (error.response) {
                setErr("Поле не может быть пустым")
            }
        }
    };

    return (
        <div className={s.container}>
            <h2 className={s.LoginLabel}>Добавьте ссылку на картинку</h2>
            <form onSubmit={handleSubmit}>
                <div className={classes.formGroup}>
                    <TextField
                        label="Ссылка"
                        variant="outlined"
                        value={Link}
                        onChange={(e) => setLink(e.target.value)}
                    />
                </div>

                <Button
                    type="submit"
                    variant="contained"
                    color="primary"
                    className={classes.submitButton}
                >
                    Добавить
                </Button>
            </form>
            <div className={s.err}>{err}</div>
        </div>
    );
};

export default AddImage;