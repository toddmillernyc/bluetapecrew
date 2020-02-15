import React, { useState } from 'react'
import PropTypes from 'prop-types'
import CategoryView from './CategoryView'
import CategoryEdit from './CategoryEdit'

const Category = ({ id, imageId, name }) => {
    const [isEditMode, setIsEditMode] = useState(false)
    const [name, setName] = useState("")
    const [published, setPublished] = useState(false)

    function handleCancelEditClick() { 
        setIsEditMode(false)
        setPublished(published)
        setName(name)
    }

    function handleSave() {
        setIsEditMode(false)
        this.props.onSaveCategory({
            id: id,
            name: name,
            published: published,
            imageId: imageId
        })
    }

    return (
            isEditMode
            ?
            <CategoryEdit
                id={id}
                imageId={imageId}
                name={name}
                published={published}
                onPublishChange={() => setPublished(!published)}
                onCancelEditClick={handleCancelEditClick}
                onNameChange={(event) => setName(event.target.value )}
                onSave={handleSave}>
            </CategoryEdit>
            :
            <CategoryView
                {...this.props}
                onPublishChange={() => setPublished(!published)}
                onEditClick={() => setIsEditMode(true) }>
            </CategoryView>
    )
}

Category.propTypes = {
    id: PropTypes.number.isRequired,
    imageId: PropTypes.number,
    name: PropTypes.string.isRequired,
    published: PropTypes.bool.isRequired,
    onSaveCategory: PropTypes.func.isRequired
}
export default Category