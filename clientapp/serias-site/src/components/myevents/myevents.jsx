import * as React from 'react';
import s from './myevents.module.css'
import Title from "../home/Title/title";
import Slider from "../utils/slider/slider";
import {useNavigate} from "react-router";
import {useEffect, useState} from "react";
import {EventAPI} from "../../api/api";


function Myevents(props) {
    const navigate = useNavigate();

    let [events, setEvents] = useState(null)

    let getEvents = async () => {
        try {
            const data = await EventAPI.GetMyEvents("")
            console.log(data.data)
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
        
            {
                events ?
                    <div className={s.slider}>
                        <Slider data={events} admin={false}/>
                    </div> :
                    <span>Loading...</span>
            }
        </div>
      </div>
    );
}

export default Myevents;