import React from 'react';
import s from './title.module.css';
import Button from '@mui/material/Button';

import {createTheme, ThemeProvider} from '@mui/material/styles';
import {amber, purple} from '@mui/material/colors';
import {NavLink} from "react-router-dom";

const theme = createTheme({
    palette: {
        mode: 'dark',
        primary: amber,
        secondary: purple,
    },
});

const Title = (props) => {
    return (
        <div className={s.section__container}>
            <ThemeProvider theme={theme}>
                <div className={s.container}>
                    <div className={s.section__data}>
                            <h2 className={s.section__title}>
                                {props.title}
                            </h2>
                        {
                            props.content !== "" ?
                                <p className={s.section__text}>{props.content}</p> :
                                <p className={s.section__text}>
                                    <p>
                                        Добро пожаловать на наш сайт, посвященный захватывающим мероприятиям и событиям, 
                                        которые вдохновляют и объединяют людей. Здесь вы найдете информацию о различных культурных, 
                                        спортивных и развлекательных событиях, которые оставляют яркие впечатления и создают 
                                        незабываемые моменты.</p>
                                    <p>Наша команда экспертов подготовила для вас подробные обзоры мероприятий, включая информацию 
                                        о датах, местах проведения, участниках и интересные факты, которые помогут вам лучше понять 
                                        каждое событие.</p>
                                    <p>Погрузитесь в мир удивительных событий, открывайте для себя новые возможности и наслаждайтесь уникальными 
                                        моментами вместе с нами!</p>
                                </p>
                        }

                        {
                            !props.image ?
                                <></> : <img src={props.image}></img>
                        }

                        {
                            props.button === "" ?
                                <></> :
                                <NavLink to={"/events"}><Button variant="outlined">{props.button}</Button></NavLink>
                        }

                    </div>
                </div>
            </ThemeProvider>
        </div>
    );
};

export default Title;