import React, { useEffect, useState } from 'react'
import PropTypes from 'prop-types';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { Button } from 'react-bootstrap'
import * as context from '../../apiContext'

const ImagePicker = ({ imageId, imageIds, isEditMode }) => {

  const [imageIndex, setImageIndex] = useState(imageId ?? 0)
  const [selectedImage, setSelectedImage] = useState(imageId)

  useEffect(() => {
    async function init() {

      // if(imageIds.length > 0) {
      //   const image = await context.images.get(imageIds[imageIndex])
      //   console.log(image)
      // }

      if(isEditMode) {

        if(!selectedImage && imageIds.length > 0) {
          const image = await context.images.get(imageIds[0])
          if(image) {
            setSelectedImage(image)
          }
        }

      }
    }
    init()
  }, [isEditMode]);

  useEffect(() => { console.log("id change")  }, [imageId]);

  async function nextImage() {
    const index = imageIndex + 1
    setImageIndex(index)
    var image = await context.images.get(imageIds[index])
    setSelectedImage(image)
  }

  async function previousImage() {
    const index = imageIndex - 1
    setImageIndex(index)
    var image = await context.images.get(imageIds[index])
    setSelectedImage(image)
  }

  return (
    isEditMode
      ? <>
        <Button
          size="sm"
          variant="outline-info"
          onClick={previousImage}
          disabled={
            imageIndex === 0
          }>
            <FontAwesomeIcon icon={"arrow-left"} />
        </Button>

        <img
          height={100}
          src={
            selectedImage
              ? `data:image/png;base64,${selectedImage.imageData}`
              : "https://via.placeholder.com/100"
          } 
          style={{margin: "1%"}}
        />

        <Button 
          size="sm"
          variant="outline-info"
          onClick={nextImage}
          disabled={
            imageIndex + 1 === imageIds.length
          }>
          <FontAwesomeIcon icon={"arrow-right"} />
        </Button>
      </>
      : <>
        {
          imageId > 0
            ? { imageId }
            : <img src="https://via.placeholder.com/100" />
        }
      </>
  )
}

ImagePicker.propTypes = {
  id: PropTypes.number,
  isEditMode: PropTypes.bool.isRequired,
  imageIds: PropTypes.arrayOf(PropTypes.number)
}

export default ImagePicker