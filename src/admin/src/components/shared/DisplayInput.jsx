import PropTypes from 'prop-types';
import React from 'react'

const DisplayInput = ({ isEditMode, value, changeCallback }) => {
    return (
        isEditMode
            ? <input
                className="form-control"
                defaultValue={value}
                onChange={changeCallback} />
            : value
    )
}

DisplayInput.propTypes = {
    changeCallback: PropTypes.func.isRequired,
    isEditMode: PropTypes.bool.isRequired,
    value: PropTypes.string.isRequired
}

export default DisplayInput