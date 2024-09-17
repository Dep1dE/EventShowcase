import * as React from 'react';
import s from './events.module.css'
import Title from "../home/Title/title";
import Slider from "../utils/slider/slider";
import {useNavigate} from "react-router";
import {useEffect, useState} from "react";
import {EventAPI} from "../../api/api";


function Events(props) {
    const navigate = useNavigate();

    let [serials, setEvents] = useState(null)

    let getEvents = async () => {
        try {
            const data = await EventAPI.GetEvents()
            setEvents(data.data)
        } catch (error) {

        }
    }

    useEffect(() => {
        if (!props.user.isAuth) {
             navigate("/login")
         }
        getEvents()
    }, []);

    return (
      <div className={s.Films}>
        <div>
            <Title
                title={"Основная информация: "}
                content={""}
                button=""
            />
            {
                serials ?
                    <div className={s.slider}>
                        <Slider data={serials} admin={false}/>
                    </div> :
                    <span>Loading...</span>
            }
        </div>
      </div>
    );
}

export default Events;