import React, { useState, useEffect } from 'react';

import OilPump from './OilPump';

import '../styles/Field.css';

export default function Field(props) {
    return (
        <div className='field d-flex flex-wrap'>
            {
                props.oilPumps.map(op => (
                    <OilPump key={op.id} oilPump={op} refreshBarrels={props.refreshBarrels} />
                ))
            }
        </div>
    );
}
