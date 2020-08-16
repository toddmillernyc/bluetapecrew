import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import PropTypes from 'prop-types';
import React from 'react'
import Switch from 'react-switch'

const DisplaySwitch = ({ value, isEditMode, changeCallback }) => {
    return (
        isEditMode
            ? <Switch
                height={21}
                width={42}
                checked={value}
                onChange={changeCallback}>
            </Switch>
            : <FontAwesomeIcon
                className={value ? "text-success" : "text-danger"}
                icon={value ? "check" : "times"} />
    )
}

DisplaySwitch.prototypes = {
    value: PropTypes.bool.isRequired,
    isEditMode: PropTypes.bool.isRequired,
    changeCallback: PropTypes.func.isRequired
}

export default DisplaySwitch