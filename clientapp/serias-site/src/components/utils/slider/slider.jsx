import React, {useEffect, useState} from 'react';
import {Carousel} from 'react-responsive-carousel';
import 'react-responsive-carousel/lib/styles/carousel.min.css';
import s from './slider.module.css'
import Button from "@mui/material/Button";
import {createTheme, ThemeProvider} from "@mui/material/styles";
import {amber, purple} from "@mui/material/colors";
import {NavLink} from "react-router-dom";
import TextField from '@material-ui/core/TextField';
import { makeStyles } from '@material-ui/core/styles';
import {EventAPI} from "../../../api/api";

const theme = createTheme({
    palette: {
        mode: 'dark',
        primary: amber,
        secondary: purple,
    },
});

const useStyles = makeStyles((theme) => ({
    root: {
        '& .MuiTextField-root': {
            margin: theme.spacing(1),
            width: '25ch',
            backgroundColor: '#444',
        },
    },
}));
const GetItemsArr = (props, name, text) => {

    


    return props.data.filter((e) => e.title.toLowerCase().includes(name.toLowerCase())).map((e) => {
        return (
            <div>
                <p className={s.Name}>{e.title}</p>
                <img src={e.Photo} alt={e.title}/>
                <div className={s.movie_button}>
                <NavLink to={"/event/"+e.id} >
                    <Button  variant="outlined">
                        {
                            props.admin ? "Редактировать" : text
                        }
                    </Button>
                </NavLink>
                
                </div>
            </div>
        )
    })
}

const Slider = (props) => {

    //\props.admin

    const [idx, setIdx] = useState(0);
    const [selectedItem, setSelectedItem] = useState(0);

    useEffect(() => {
        setSelectedItem(idx);
    }, [idx]);
    console.log(idx)
    const [name, setName] = useState("");

    let ItemsArr = GetItemsArr(props, name, "Узнать подробнее")

    const handleChange = (event) => {
        setName(event.target.value);
    };

    const handleClick = (event) => {
        setIdx(0);
    };

    const classes = useStyles();
    return (
        <div className={s.container}>
            <ThemeProvider theme={theme}>
                <div className={s.Search}>
                    <span className={s.SearchLable}>{"Поиск"}</span>
                    <form className={classes.root} noValidate autoComplete="off">
                        <TextField
                            label={"Название мероприятия"}
                            variant="outlined"
                            InputProps={{
                                style: {
                                    color: '#777',
                                },
                            }}
                            value={name}
                            onChange={handleChange}
                            onClick={handleClick}
                        />
                    </form>

                </div>
                <div className={s.wrapper}>
                    {ItemsArr.length > 0 ?
                        <Carousel className={s.Slider}
                        selectedItem={selectedItem}>
                            {ItemsArr}
                        </Carousel>
                        : <p style={{color:"#777"}}>{"Мероприятия с таким названием не найдены..."}</p>
                    }
                    </div>
            </ThemeProvider>
        </div>

);
};

export default Slider;
