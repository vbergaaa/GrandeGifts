import data from './data.json';
import axios from 'axios';

export default class ApiService {
    baseUrl = "";
    constructor() {
        this.baseUrl = "https://grandegiftsryanvandenberg.azurewebsites.net/"; 
    }

    GetAllHampers(callback) {
        axios.get(this.baseUrl + "api/hampers")
            .then((response) => {
                callback(response.data);
            })
            .catch((error) => {
                console.log(error);
            });
    }

    GetHampersByCategory(keyword, callback) {
        axios.get(this.baseUrl + "api/hampers/" + keyword)
            .then((response) => {
                callback(response.data);
            })
            .catch((error) => {
                console.log(error);
            });
    }

    GetData(keyword, callback) {
        let results = [];
        
        callback(data);
    }

    GetAllData(callback) {
        callback(data);
    }

}