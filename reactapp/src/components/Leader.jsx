import React, { useState, useEffect } from 'react';

import '../styles/Leader.css';

export default function Leader(props) {
    return (
        <div className='leader'>
            <div className='leader-index'>{props.index}</div>
            <span className='leader-name'>{props.leader.name}</span>
            <span className='leader-stats'>
                {`${props.leader.fieldsCount} | ${props.leader.oilPumpsCount}`}
            </span>
            <span className='leader-barrels'>
                {props.leader.totalBarrels}
            </span>
        </div>
    );
}
