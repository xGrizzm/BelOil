import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

import ApiService from '../api/ApiService';
import URLConstants from '../utils/constants/URLConstants';
import JwtHelper from '../utils/helpers/JwtHelper';

import FieldItem from '../components/FieldItem';
import Field from '../components/Field';

import '../styles/Game.css';

export default function GamePage() {
    const [state, setState] = useState({ fields: [], selectedField: null, barrels: 0, fieldPrice: 0, isLoading: false });

    const navigate = useNavigate();

    const logoutButtonClick = () => {
        JwtHelper.removeToken();
        navigate("/authorization");
    }

    const fieldItemClick = (field) => {
        setState({ ...state, selectedField: field})
    }

    const oilPumpClick = async (fieldId, oilPumpId) => {
        try {
            setState({ ...state, isLoading: true });
            const data = await ApiService.put(URLConstants.COLLECT_OIL_PUMP_URL.format(fieldId, oilPumpId));
            state.fields.find(f => f.id == fieldId).oilPumps.find(op => op.id == oilPumpId).nextPumping = data.nextPumping;
            setState({ ...state, barrels: data.barrels, isLoading: false });
        } catch (error) {
            
        }
    }

    const buyFieldClick = async () => {
        try {
            setState({ ...state, isLoading: true });
            const data = await ApiService.post(URLConstants.BUY_FIELD_URL);
            state.fields.push(data.item);
            setState({ ...state, barrels: data.barrels, isLoading: false });
        } catch (error) {
            
        }
    }

    const buyOilPumpClick = async (fieldId) => {
        try {
            setState({ ...state, isLoading: true });
            const data = await ApiService.post(URLConstants.BUY_OIL_PUMP_URL.format(fieldId));
            state.fields.find(f => f.id == fieldId).oilPumps.push(data.item);
            setState({ ...state, barrels: data.barrels, isLoading: false });
        } catch (error) {
            
        }
    }

    const refreshBarrels = async (barrels) => {
        setState({ ...state, barrels: barrels });
    }

    const getGame = async () => {
        const data = await ApiService.get(URLConstants.GET_GAME_URL);
        setState({ ...state, barrels: data.barrels, fields: data.fields, fieldPrice: data.fieldPrice });
    }

    useEffect(() => {
        getGame();
    }, []);

    return (
        <div className='d-flex flex-column vh-100' style={{ backgroundColor: '#363636' }}>
            <div className='mt-4 mx-4'>
                <button className='btn btn-success float-end' onClick={logoutButtonClick}>Выйти</button>
                <div style={{ color: '#198754', fontSize: 25 }}>{`У вас есть ${state.barrels}`}</div>
            </div>
            <div className='mt-4 mx-4 h-100 d-flex align-items-center'>
                <div className='col-4'>

                </div>
                <div className='col-8'>
                    <div className='d-flex'>
                        {
                            !state.selectedField
                                ? <button className='me-1 btn btn-success' onClick={buyFieldClick} disabled={state.fieldPrice > state.barrels || state.isLoading}>{`Купить месторождение (${state.fieldPrice})`}</button>
                                : state.selectedField.oilPumps.length < 4 
                                    ? <button className='me-1 btn btn-success' onClick={() => buyOilPumpClick(state.selectedField.id)} disabled={state.selectedField.oilPumpPrice > state.barrels || state.isLoading}>{`Купить насос (${state.selectedField.oilPumpPrice})`}</button>
                                    : null
                        }
                        { state.selectedField ? <button className='btn btn-success ms-auto' onClick={() => setState({...state, selectedField: null})}>Назад</button> : null }
                    </div>
                    <div className='mt-3' style={{ overflowY: 'auto', height: '550px'}}>
                        {
                            state.selectedField 
                                ? <Field id={state.selectedField.id} oilPumps={state.selectedField.oilPumps} isLoading={state.isLoading} oilPumpClick={oilPumpClick} />
                                : state.fields.map(f => (
                                    <FieldItem key={f.id} field={f} fieldItemClick={fieldItemClick} />
                                ))
                        }
                    </div>
                </div>
            </div>
        </div>
    );
}
