import React, { useState, useEffect } from 'react';

import '../styles/FieldItem.css';

export default function FieldItem(props) {
    return (
        <button className='field-item btn btn-success' onClick={() => props.fieldItemClick(props.field)}>
            {'Месторождение ' + props.field.id}
            <span className='float-end'>{`${props.field.oilPumps.length} насоса`}</span>
        </button>
    );
}
