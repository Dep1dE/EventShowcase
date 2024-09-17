import {NavLink} from 'react-router-dom';
import s from './header.module.css';
import {useEffect, useState} from 'react';
import {AuthAPI} from "../../../api/api";
function Header(props) {
    console.log(props.user.isAdmin)
    const LINKS_CLASS_NAME = "d-flex " + s.header_container + " "
    let [isShowen, setIsShowen] = useState(true)
    let [links_class_name, setClassName] = useState(LINKS_CLASS_NAME)

    const logout = async () =>{
        try {
            const user = await AuthAPI.Logout();
            props.setUser({ isAuth: false, id: 0, email: "", isAdmin: false})
        } catch (error) {
        }
    }

    function burger_click() {
        setIsShowen(!isShowen)
        let addiction = ""
        if (isShowen) {
            addiction = s.showen
        }
        setClassName(LINKS_CLASS_NAME + addiction)
    }

    return (
        
            <header>
                <div className={s.container}>
                    <div className={"d-flex " + s.header_box}>
                        <div className={s.burger_button_container}>
                            <span onClick={burger_click} className={s.burger_button}></span>
                        </div>
                        <div className={links_class_name}>
                            <NavLink className={s.menu_btn} to={"/homepage"}>Главная</NavLink>
                            <NavLink className={s.menu_btn} to={"/events"}>Мероприятия</NavLink>
                            <NavLink className={s.menu_btn} to={"/myevents"}>Мои мероприятия</NavLink>
                            {
                                
                                props.user.isAdmin ?
                                    <NavLink className={s.menu_btn} to={"/admin"}>Добавить мероприятие</NavLink> : <></>
                            }
                            <NavLink className={s.menu_btn} to={"https://github.com/Dep1dE"}>Разработчикам</NavLink>
                        </div>
                        <div>
                            {
                                !props.user.isAuth ?
                                    <NavLink className={s.menu_btn} to={"/login"}>Вход</NavLink> :
                                    <span
                                        className={s.menu}>({props.user.email})
                                        <NavLink onClick={logout} className={s.menu_btn} to={"/login"}>Выход</NavLink>
                                    </span>
                            }
                        </div>
                    </div>
                </div>
            </header>
        
    );
}

export default Header;