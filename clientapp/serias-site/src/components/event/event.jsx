import * as React from "react";
import {useNavigate, useParams} from "react-router";
import Title from "../home/Title/title";
import s from "./event.module.css";
import i from '../../components/utils/header/header.module.css';
import Trailer from "../home/Trailer/trailer";
import {createTheme, ThemeProvider} from "@mui/material/styles";
import {amber, purple} from "@mui/material/colors";
import {useEffect, useState} from "react";
import {EventAPI} from "../../api/api";
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';
import {NavLink} from 'react-router-dom';



const apiKey = 'YOUR_API_KEY';

const theme = createTheme({
    palette: {
        mode: 'dark',
        primary: amber,
        secondary: purple,
    },
});

 


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

function Event(props) {
    const { id } = useParams();
    const navigate = useNavigate();
    const classes = useStyles();
    const currentUserId = props.user.id; 
    let [event, setEvent] = useState(null)
    let [images, setImages] = useState(null)
    let [userIds, setIds] = useState(null)
    let [maxUserCount, setmaxUserCount] = useState(null)
    let [currentUserCount, setcurrentUserCount] = useState(null)

    let getEvent = async () => {
        try {
            const data = await EventAPI.GetEvent(id)
            const dataImages = await EventAPI.GetEventImages(id)
            setEvent(data.data)
            setImages(dataImages.data)
            setIds(data.data.users.map(user => user.id))
            setmaxUserCount(data.data.maxUserCount)
            setcurrentUserCount(userIds.length)

        } catch (error) {

        }
    }
    const handleSubscribe = async () => {
      try {
        const data = await EventAPI.RegisterToEvent(id)
        navigate('/events');
        }
     catch (error) {
        console.error("Ошибка:", error);
        alert("Произошла ошибка");
      }
  };
  const handleUnsubscribe = async () => {
        try {
        const data = await EventAPI.Unsubscribe(currentUserId, id)
        navigate('/events');
        }
     catch (error) {
        console.error("Ошибка:", error);
        alert("Произошла ошибка");
      }
    };


    const handleDelete = async () => {
    if (window.confirm("Вы уверены, что хотите удалить это мероприятие?")) {
      try {
        const data = await EventAPI.DeleteEvent(id)
        navigate('/events');
        }
     catch (error) {
        console.error("Ошибка:", error);
        alert("Произошла ошибка");
      }
    }
  };


    useEffect(() => {
        if (!props.user.isAuth) {
            navigate("/login")
        }
        getEvent()
    }, []);


    return (
        <>
        { event ?
                <div>
                    

                    <div className={s.container}>
                        <div className={s.wrapper}>
                            <div className={s.SerialInfo}>
                                <h3><span className={s.Hitem}>{"Основная информация: "}</span></h3>
                                <p><span className={s.item}>{"Название: "}</span>{event.title}</p>
                                <p><span className={s.item}>{"Описание: "}</span>{event.description}</p>
                                <p><span className={s.item}>{"Категория: "}</span>{event.category}</p>
                                <p><span className={s.item}>{"Место проведения: "}</span>{event.location}</p>
                                <p><span className={s.item}>{"Дата: "}</span>{new Date(event.date).toLocaleDateString('ru-RU', {
                                        day: '2-digit',
                                        month: '2-digit',
                                        year: 'numeric'
                                    })}</p>
                            </div>
                        </div>

                                    {userIds.includes(currentUserId) ? (
                <button onClick={handleUnsubscribe}>Отказаться от участия</button>
            ) : (
                currentUserCount < maxUserCount ? ( 
                    <button onClick={handleSubscribe}>Записаться на мероприятие</button>
                ) : (
                    <button disabled>Нет свободных мест</button> 
                )
            )}
                    </div>
                     <div>
                        <div className={s.container}>
                            {images.length > 0 ? (
                            images.map((image) => (
                                <p><img src={image.link} alt="" style={{ width: '400px', height: 'auto' }} /></p>
                            ))
                            ) : (
                            <p>Нету изображений к этому мероприятию</p>
                            )}
                        
                    {
                     props.user.isAdmin ?
                                    <NavLink className={i.menu_btn} to={`/addimage/${event.id}`}> Добавить картинку</NavLink>    
                                    
                                    : <></>}

                                    {
                     props.user.isAdmin ?
                                    <NavLink className={i.menu_btn} to={`/editevent/${event.id}`}> Редактировать мероприятие</NavLink>    
                                    
                                    : <></>}
                                    {
                                    props.user.isAdmin ?
                                    <button className={i.menu_btn} onClick={handleDelete}>
                                        Удалить мероприятие
                                    </button>
                                    : <></>}
                    </div>
                    </div>
                </div>  :
                <span>Loading...</span>
        }
        </>
    );
}
export default Event;
