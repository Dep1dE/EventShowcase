import React from 'react';
import YouTube from "react-youtube";
import s from './trailer.module.css';
import {useTranslation} from "react-i18next";
import {Card, CardContent} from "@mui/material";

const Trailer = (props) => {
    return (
        <div className={s.trailer__container}>
            <h2 className={s.trailer__title}>{"Трейлер"}</h2>
            <div className={s.trailer__block}>
                <CardContent className={s.custom__content}>
                    <YouTube
                        className={s.trailer__youtube}
                        videoId={props.trailerID}
                    />
                </CardContent>
            </div>
        </div>
    );
};

export default Trailer;