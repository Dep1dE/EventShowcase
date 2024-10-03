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
    const [file, setFile] = useState(null);
    const [err, setErr] = useState('');

    let navigate = useNavigate()

    const handleFileChange = (event) => {
        setFile(event.target.files[0]);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        if (!file) {
            setErr("Пожалуйста, выберите файл");
            return;
        }
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onloadend = async () => {
            const base64String = reader.result.split(',')[1];
            try {
                const data = {
                    IdEvent: id,
                    ImageData: base64String,  
                    ImageType: file.type
                };

                console.log(data.IdEvent, data.ImageData, data.ImageType);
                await EventAPI.AddImage(id, data.ImageData, data.ImageType);
                navigate("/events");
            } catch (error) {
                if (error.response) {
                    setErr("Произошла ошибка при загрузке изображения");
                }
            }
        };

        reader.onerror = () => {
            setErr("Ошибка при чтении файла.");
        };
    };


    return (
        <div className={s.container}>
            <h2 className={s.LoginLabel}>Добавьте изображение</h2>
            <form onSubmit={handleSubmit}>
                <div className={classes.formGroup}>
                     <input
                        type="file"
                        accept="image/*"
                        onChange={handleFileChange}
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