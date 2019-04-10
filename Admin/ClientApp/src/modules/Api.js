
export const getCategories = async() => {
    const response =  await fetch('http://localhost/api/categories')
    const data = await response.json()
    return data
}