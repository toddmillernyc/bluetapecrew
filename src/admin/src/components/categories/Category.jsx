import React from 'react'
import PropTypes from 'prop-types'
import CategoryView from './CategoryView'
import CategoryEdit from './CategoryEdit'

class Category extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            isEditMode: false,
            isPublished: props.published,
            name: props.name
        }
        this.handleCancelEditClick = this.handleCancelEditClick.bind(this);
        this.handleEditClick = this.handleEditClick.bind(this);
        this.handleNameChange = this.handleNameChange.bind(this);
        this.handlePublishChange = this.handlePublishChange.bind(this);
        this.handleSave = this.handleSave.bind(this);
    }

    handleCancelEditClick() { 
        this.setState({
            isEditMode: false,
            isPublished: this.props.published,
            name: this.props.name
        }) 
    }
    handleEditClick() { this.setState({ isEditMode: true }) }
    handleNameChange(event) { this.setState({ name: event.target.value })}
    handlePublishChange() { this.setState({ isPublished: !this.state.isPublished }) }
    handleSave() {
        this.setState({ isEditMode: false }) 
        this.props.onSaveCategory({
            id: this.props.id,
            name: this.state.name,
            published: this.state.published,
            imageId: this.props.imageId
        });
    }

    render() {
        return (
            this.state.isEditMode
                ?
                <CategoryEdit
                    id={this.props.id}
                    imageId={this.props.imageId}
                    name={this.state.name}
                    isPublished={this.state.isPublished}
                    onPublishChange={this.handlePublishChange}
                    onCancelEditClick={this.handleCancelEditClick}
                    onNameChange={this.handleNameChange}
                    onSave={this.handleSave}>
                </CategoryEdit>
                :
                <CategoryView
                    {...this.props}
                    onPublishChange={this.handlePublishChange}
                    onEditClick={this.handleEditClick}>
                </CategoryView>
        )
    }
}

Category.propTypes = {
    id: PropTypes.number.isRequired,
    imageId: PropTypes.number,
    name: PropTypes.string.isRequired,
    published: PropTypes.bool.isRequired,
    onSaveCategory: PropTypes.func.isRequired
}
export default Category