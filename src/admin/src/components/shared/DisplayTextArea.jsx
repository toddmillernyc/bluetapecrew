import PropTypes from 'prop-types';
import React from 'react'

const DisplayTextArea = ({ isEditMode, value, changeCallback }) => {

    return (
        isEditMode
            ? <textarea
                className="form-control"
                defaultValue={value}
                onChange={changeCallback}>
                </textarea>
            : value
    )
}

DisplayTextArea.propTypes = {
    changeCallback: PropTypes.func.isRequired,
    isEditMode: PropTypes.bool.isRequired,
    value: PropTypes.string.isRequired
}

export default DisplayTextArea