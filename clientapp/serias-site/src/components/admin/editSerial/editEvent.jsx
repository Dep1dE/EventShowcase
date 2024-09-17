import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import s from './editEvent.module.css';
import { EventAPI } from '../../../api/api';
import { useNavigate, useParams } from 'react-router';
import dayjs from 'dayjs';

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

const EditEvent = (props) => {
    const classes = useStyles();
    const [Title, setTitle] = useState('');
    const [Description, setDescription] = useState('');
    const [Location, setLocation] = useState('');
    const [Category, setCategory] = useState('');
    const [selectedDate, setSelectedDate] = useState('');
    const [maxUserCount, setMaxUserCount] = useState('');
    const [err, setErr] = useState('');
    const navigate = useNavigate();
    const { id } = useParams(); 

    useEffect(() => {
        const loadEvent = async () => {
            try {
                const event = await EventAPI.GetEventById(id); 
                setTitle(event.title);
                setDescription(event.description);
                setLocation(event.location);
                setCategory(event.category);
                setSelectedDate(dayjs(event.date).toISOString());
                setMaxUserCount(event.maxUserCount);
            } catch (error) {
                console.error("Ошибка при загрузке мероприятия:", error);
            }
        };
        loadEvent();
    }, [id]);

    const handleDateChange = (event) => {
        const date = dayjs(event.target.value).toISOString();
        setSelectedDate(date);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        try {
            console.log(id)
            await EventAPI.UpdateEvent(id, Title, Description, selectedDate, Location, Category, maxUserCount);
            navigate("/events");
        } catch (error) {
            if (error.response) {
                setErr("Ошибка при редактировании мероприятия");
            }
        }
    };

    return (
        <div className={s.container}>
            <h2 className={s.LoginLabel}>Редактирование мероприятия</h2>
            <form onSubmit={handleSubmit}>
                <div className={classes.formGroup}>
                    <TextField
                        label="Название"
                        variant="outlined"
                        value={Title}
                        onChange={(e) => setTitle(e.target.value)}
                    />
                </div>
                <div className={classes.formGroup}>
                    <TextField
                        label="Описание"
                        variant="outlined"
                        value={Description}
                        onChange={(e) => setDescription(e.target.value)}
                    />
                </div>
                <div>
                    <TextField
    label="Выберите дату и время"
    type="datetime-local"
    variant="outlined"
    value={dayjs(selectedDate).format('YYYY-MM-DDTHH:mm')}  
    onChange={handleDateChange}  
    InputLabelProps={{
        shrink: true,
    }}
    style={{ marginBottom: '20px' }}
    fullWidth
/>
                </div>
                <div className={classes.formGroup}>
                    <TextField
                        label="Место проведения"
                        variant="outlined"
                        value={Location}
                        onChange={(e) => setLocation(e.target.value)}
                    />
                </div>
                <div className={classes.formGroup}>
                    <TextField
                        label="Категория"
                        variant="outlined"
                        value={Category}
                        onChange={(e) => setCategory(e.target.value)}
                    />
                </div>
                <div className={classes.formGroup}>
                    <TextField
                        label="Максимальное количество участников"
                        type="number"
                        variant="outlined"
                        value={maxUserCount}
                        onChange={(e) => setMaxUserCount(e.target.value)}
                        fullWidth
                        style={{ marginTop: '20px' }}
                    />
                </div>
                <Button
                    type="submit"
                    variant="contained"
                    color="primary"
                    className={classes.submitButton}
                >
                    Сохранить изменения
                </Button>
            </form>
            <div className={s.err}>{err}</div>
        </div>
    );
};

export default EditEvent;
