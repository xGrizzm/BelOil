import React, { useState, useEffect } from 'react';

import OilBarrelWhite from '../assets/oil-barrel-white.png';

import '../styles/OilPump.css';

export default function OilPump(props) {
    const [state, setState] = useState({ isDisabled: false, time: 'Готово' });

    const oilPumpClick = async () => {
        if (state.isDisabled) return; 

        props.oilPumpClick(props.oilPump.fieldId, props.oilPump.id);
    }
    
    const refreshOilPumpTime = () => {
        const date = new Date();
        const currentDate = new Date(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(), date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds());
        const nextPumping = new Date(props.oilPump.nextPumping);
        
        var milliseconds = nextPumping - currentDate;
        if (milliseconds >= 1000) {
            let seconds = parseInt(milliseconds / 1000);
            let minutes = parseInt(seconds / 60);
            let hours = parseInt(minutes / 60);
            minutes -= hours * 60;
            seconds -= minutes * 60 + hours * 3600;
            setState({ 
                ...state, 
                isDisabled: true, 
                time: `${hours == 0 ? '' : hours > 9 ? hours + ':' : '0' + hours + ':' }${minutes > 9 ? minutes : '0' + minutes}:${seconds > 9 ? seconds : '0' + seconds}` 
            });
            milliseconds -= 1000;

            var timer = setInterval(() => {
                let seconds = parseInt(milliseconds / 1000);
                let minutes = parseInt(seconds / 60);
                let hours = parseInt(minutes / 60);
                minutes -= hours * 60;
                seconds -= minutes * 60 + hours * 3600;
                if (seconds > 0 || minutes > 0 || hours > 0) {
                    setState({ 
                        ...state, 
                        isDisabled: true, 
                        time: `${hours == 0 ? '' : hours > 9 ? hours + ':' : '0' + hours + ':' }${minutes > 9 ? minutes : '0' + minutes}:${seconds > 9 ? seconds : '0' + seconds}` 
                    });
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
