import React, { useState, useEffect } from 'react';

import OilPumpWhite from '../assets/oil-pump-white.png';

import '../styles/FieldItem.css';

export default function FieldItem(props) {
    return (
        <button className='field-item btn btn-success' onClick={() => props.fieldItemClick(props.field)}>
            <span className='field-item-name'>{`Месторождение ${props.index}`}</span>
            <div className='field-item-oil-pumps float-end'>
                {`${props.field.oilPumps.length}`}
                <img src={OilPumpWhite} width='35px' />
            </div>
        </button>
    );
}
