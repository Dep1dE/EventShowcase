import Header from "./components/utils/header/header";
import Footer from "./components/utils/footer/footer";
import {Route, Routes} from "react-router-dom";
import Home from "./components/home/home";
import Events from "./components/events/events";
import Event from "./components/event/event";
import Myevents from "./components/myevents/myevents";
import React, {Suspense, useEffect, useState} from "react";
import Login from "./components/auth/login/login";
import Registration from "./components/auth/registration/registration";
import AddEvent from "./components/admin/addEvent/addEvent";
import EditEvent from "./components/admin/editSerial/editEvent";
import AddImage from "./components/admin/addImage/addImage";

import {AuthAPI, EventAPI} from "./api/api";

function App() {

    let [init, setInit] = useState(false)

    let [user, setUser] =
        useState({ isAuth: false, id: 0, email: "", isAdmin: false });


    let auth = async () => {
        try {
            const user = await AuthAPI.Auth();
            let userData = user.data
            console.log("dddddddd")
        
           // console.log(userData.isAdmin)
            //let isAdmin = userData.isAdmin ? true : false
            setUser({ isAuth: true, id: userData.id, email: userData.email, isAdmin: userData.isAdmin})

        } catch (error) {
            
        }
        setInit(true)
    }

    useEffect(() => {
        auth()
    }, []);

    return (
        <div>
            { init ?
                <>
                    <Header setUser={setUser} user={user}/>
                    <Suspense fallback={<div>Loading...</div>}>
                        <Routes>
                            <Route path='homepage' element={<Home/>}/>

                            <Route path='myevents' element={<Myevents user={user}/>}/>

                            <Route path='events' element={<Events user={user}/>}/>

                            <Route path='admin' element={<AddEvent user={user}/>}/>

                            <Route path='login' element={<Login setUser={setUser}/>}/>

                            <Route path='/addimage/:id' element={<AddImage setUser={setUser}/>}/>

                            <Route path='/editevent/:id' element={<EditEvent setUser={setUser}/>}/>

                            <Route path='registration' element={<Registration/>}/>

                            <Route path='event/:id' element={<Event user={user}/>}/>
                        </Routes>
                    </Suspense>
                    <Footer/>
                </> : <span>Loading...</span>

            }
        </div>
    );
}
export default App;