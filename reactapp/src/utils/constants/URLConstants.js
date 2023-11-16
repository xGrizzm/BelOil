const BASE_URL = "https://localhost:44368";

const RequestURL = {
    LOGIN_URL: `${BASE_URL}/authorization/login/`,
    
    GET_GAME_URL: `${BASE_URL}/game/`,
    COLLECT_OIL_PUMP_URL: `${BASE_URL}/game/fields/{0}/oilpumps/{1}/collect/`,
    BUY_FIELD_URL: `${BASE_URL}/game/fields/buy/`,
    BUY_OIL_PUMP_URL: `${BASE_URL}/game/fields/{0}/oilpumps/buy/`,

    GET_LEADERBOARD_URL: `${BASE_URL}/leaderboard/`
}

export default RequestURL