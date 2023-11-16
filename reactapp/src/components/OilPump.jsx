import React, { useState, useEffect } from 'react';

import OilBarrelWhite from '../assets/oil-barrel-white.png';

import '../styles/OilPump.css';

export default function OilPump(props) {
    const [state, setState] = useState({ isDisabled: true, time: '' });

    const oilPumpClick = async () => {
        if (state.isDisabled) return; 

        props.oilPumpClick(props.oilPump.fieldId, props.oilPump.id);
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
    }, [props]);

    return (
        <button className='oil-pump btn btn-success position-relative' onClick={oilPumpClick} disabled={state.isDisabled || props.isLoading}>
            <div className='barrels'>
                {props.oilPump.barrels}
                <img src={OilBarrelWhite} width='50px' height='50px' />
            </div>
            <div className='position-absolute' style={{ top: '7px', right: '10px'}}>
                {state.time}
            </div>
        </button>
    );
}
