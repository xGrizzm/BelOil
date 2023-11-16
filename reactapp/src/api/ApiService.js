import axios from 'axios'
import JwtHelper from '../utils/helpers/JwtHelper';

const ApiService = {
    setHeader() {
        let token = JwtHelper.getToken();
        axios.defaults.headers.common["Authorization"] = token ? `Bearer ${token}` : "";
    },

    errorHandler(status) {
        if (status == 401) {
            JwtHelper.removeToken();
        } else if (status == 403) {
            
        }
    },

    async get(URL) {
        this.setHeader();
        try {
            const response = await axios.get(`${URL}`);
            return response.data;
        } catch ({ response }) {
            this.errorHandler(response.status)
            throw response;
        }
    },

    async post(URL, parameters) {
        this.setHeader();
        try {
            const response = await axios.post(`${URL}`, parameters);
            return response;
        } catch ({ response }) {
            this.errorHandler(response.status)
            throw response;
        }
    },

    async put(URL, parameters) {
        this.setHeader();
        try {
            const response = await axios.put(`${URL}`, parameters);
            return response;
        } catch ({ response }) {
            this.errorHandler(response.status)
            throw response;
        }
    }
}

export default ApiService