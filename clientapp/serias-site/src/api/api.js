import axios from "axios";
import AddImage from "../components/admin/addImage/addImage";

const instance = axios.create({
    withCredentials: true,
    baseURL: 'https://localhost:5001/',
})

export const AuthAPI = {
    Auth() {
        return instance.get('auth')
    },

    ///////////////////////////////////////////////////////
    Login(email, password) {
        return instance.post('login/', {email, password})
    },
    Registration(username,email, password) {
        return instance.post('register/', {username,email, password})
    },
    Logout() {
        return instance.delete('logout/')
    },
    ///////////////////////////////////////////////////////


    GetNotifications(from) {
        return instance.get('notification/'+from)
    },
    SetNotificationsShowen(id) {
        return instance.post('notification/show/'+id)
    },
    DeleteNotification(id) {
        return instance.delete('notification/'+id)
    }
}

export const EventAPI = {
    
    GetEvent(idEvent) {
        return instance.post('get_event_by_id/',{idEvent})
    },
    GetEventImages(idEvent) {
        return instance.post('get_event_images/',{idEvent})
    },
    GetEvents() {
        return instance.get('get_all_events/')
    },

    GetMyEvents(token) {
        return instance.get('get_my_events/', {token})
    },

    RegisterToEvent(idEvent, token) {
        return instance.post('register_user_to_event/', {idEvent, token})
    },
    Unsubscribe(idUser, idEvent) {
        return instance.post('delete_user_in_event/', {idUser, idEvent})
    },
    AddEvent(title, description, date, location, category, maxUserCount) {
        return instance.post('add_new_event/', {title, description, date, location, category, maxUserCount})
    },
    UpdateEvent(Id, Title, Description, Date, Location, Category, maxUserCount) {
        return instance.post('update_event/', {Id,Title, Description, Date, Location, Category, maxUserCount})
    },
    AddImage(IdEvent, ImageData, ImageType) {
        return instance.post('add_image/', {IdEvent, ImageData, ImageType})
    },
    DeleteEvent(idEvent) {
        return instance.post('delete_event/', {idEvent})
    }

}
