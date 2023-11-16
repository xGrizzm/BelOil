const BASE_URL = "https://localhost:44368";

const RequestURL = {
    LOGIN_URL: `${BASE_URL}/authorization/login/`,
    
    GET_GAME_URL: `${BASE_URL}/game/`,
    COLLECT_OIL_PUMP_URL: `${BASE_URL}/fields/{fieldId}/oilpumps/{oilPumpId}/collect/`,
    BUY_FIELD_URL: `${BASE_URL}/fields/buy/`,
    BUY_OIL_PUMP_URL: `${BASE_URL}/fields/{fieldId}/oilpumps/buy/`
}

export default RequestURL