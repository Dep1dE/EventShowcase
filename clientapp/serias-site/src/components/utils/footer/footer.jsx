import React from 'react';
import {NavLink} from "react-router-dom";
import s from "./footer.module.css";
import {Icon} from "@mui/material";
import {Facebook, Instagram, Mail} from "@mui/icons-material";
import {Twitter} from "@material-ui/icons";

const Footer = () => {
    return (
        <footer>
            <div className={s.footer__container}>
                <div className={"d-flex justify-between"}>
                    <a className={s.teamImg} href={"https://github.com/Dep1dE"}>
                        <img className={s.footer__logo}
                             src="https://upload.wikimedia.org/wikipedia/commons/thumb/7/7d/Microsoft_.NET_logo.svg/640px-Microsoft_.NET_logo.svg.png"
                             alt="python cucold developers team"/>
                    </a>
                    <div className={s.footer__links}>
                        <NavLink to = {"https://www.instagram.com"} target = "_blank" rel = "noopener noreferrer">
                            <Icon className={s.Icon} >
                                <Instagram className={s.Contact}/>
                            </Icon>
                        </NavLink>
                        <NavLink to = {"https://facebook.com"} target = "_blank" rel = "noopener noreferrer">
                            <Icon className={s.Icon}>
                                <Facebook className={s.Contact} />
                            </Icon>
                        </NavLink>
                        
                        <NavLink to = {"https://twitter.com"} target = "_blank">
                            <Icon className={s.Icon}>
                                <Twitter className={s.Contact}/>
                            </Icon>
                        </NavLink>
                    </div>
                </div>
            </div>
        </footer>
    );
};

export default Footer;