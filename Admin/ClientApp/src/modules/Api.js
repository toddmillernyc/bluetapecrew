const baseUrl = "http://localhost/api"

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
export const getCategories = async() => await get("categories")
export const getSitSettings = async() => await get("siteSettings")

export const saveSiteSettings = async(model) => await post("siteSettings", model)