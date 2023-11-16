import React, { useState, useEffect } from 'react';

import ApiService from '../api/ApiService';
import URLConstants from '../utils/constants/URLConstants';

import '../styles/OilPump.css';

export default function OilPump(props) {
    const [state, setState] = useState({ isDisabled: true, time: '' });

    const oilPumpClick = async () => {
        if (state.isDisabled) return; 

        try {
            const data = await ApiService.put(URLConstants.COLLECT_OIL_PUMP_URL.format(props.oilPump.fieldId, props.oilPump.id));

            props.oilPump.nextPumping = data.nextPumping;
            props.refreshBarrels(data.barrels);

            refreshOilPumpTime();
        } catch (error) {
            
        }
    }
    
    const refreshOilPumpTime = () => {
        const date = new Date();
        const currentDate = new Date(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(), date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds());
        const nextPumping = new Date(props.oilPump.nextPumping);
        
        if (nextPumping > currentDate) {
            var milliseconds = nextPumping - currentDate;

            let seconds = parseInt(milliseconds / 1000);
            let minutes = parseInt(seconds / 60);
            seconds -= minutes * 60;
            setState({ ...state, isDisabled: true, time: `${minutes}:${seconds}` });
            milliseconds -= 1000;

            var timer = setInterval(() => {
                let seconds = parseInt(milliseconds / 1000);
                let minutes = parseInt(seconds / 60);
                seconds -= minutes * 60;
                if (seconds > 0) {
                    setState({ ...state, isDisabled: true, time: `${minutes}:${seconds}` });
                    milliseconds -= 1000;
                } else {
                    setState({ ...state, isDisabled: false, time: 'Готово' });
                    clearInterval(timer);
                }
            }, 1000)
        } else {
            setState({ ...state, isDisabled: false, time: 'Готово' });
        }
    }

    useEffect(() => {
        refreshOilPumpTime();
    }, []);

    return (
        <button className='oil-pump btn btn-success position-relative' onClick={oilPumpClick} disabled={state.isDisabled}>
            <span className='barrels'>{props.oilPump.barrels}</span>
            <div className='position-absolute' style={{ top: '7px', right: '10px'}}>
                {state.time}
            </div>
        </button>
    );
}
