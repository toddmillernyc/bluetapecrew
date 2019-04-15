export const baseUrl = "https://localhost:5001/api"


export const getCategories = async() => await get("categories")
export const getSiteSettings = async() => await get("sitesettings")
export const saveSiteSettings = async(model) => await post("siteSettings", model)
export const getProduct = async(id) => await get("products/"+id)
export const getProductImages = async(id) => await get("productimages/"+id)
export const getProductStyles = async(id) => await get("productstyles/" + id)
export const updateProduct = async(id, model) => await put("products/" + id, model)
export const deleteSytle = async(id) => await remove("productstyles/" + id)

const get = async(endpoint) => {
    const response =  await fetch(`${baseUrl}/${endpoint}`)
    return await response.json()
}

const post = async(endpoint, data) => {
    const result = await fetch(`${baseUrl}/${endpoint}`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    })
    return result
}

const put = async(endpoint, data) => {
    const result = await fetch(`${baseUrl}/${endpoint}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    })
    return result
}

const remove = async(id) => {
    const result = await fetch(`${baseUrl}/${id}`, {method: 'DELETE'})
    return result
}
