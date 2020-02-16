const baseUrl = "https://api.toddmiller.nyc"

export async function getCategories() {
    let response = await fetch(`${baseUrl}/categories`)
    return await response.json()
}

export async function saveCategory(category) {
    await fetch(`${baseUrl}/categories`, 
    {
      method: 'PUT',
      body: JSON.stringify(category),
      headers: {
        'Content-Type': 'application/json',
      }
    })
  }

  export async function  newCategory(category) {
    await fetch(`${baseUrl}/categories`, 
    {
      method: 'POST',
      body: JSON.stringify(category),
      headers: {
        'Content-Type': 'application/json',
      }
    })
  }