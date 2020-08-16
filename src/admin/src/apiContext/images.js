import * as client from './apiClient'

const endpoint = "images"
const imageCache = {}

export const get = async (id) => {
    
    //return imageIds if no id passed
    if(!id) return await client.get(endpoint)

    //return cached image if found
    let image = imageCache[id]
    if(image) return image

    //get image from api
    image = await client.get(`${endpoint}/${id}`)
    if(image) imageCache[id] = image
    console.log(image)
    return image
}

export const create = async (product) => await client.create(endpoint, product)
export const update = async (product) => await client.update(endpoint, product)
export const del = async (id) => await client.del(endpoint, id)
