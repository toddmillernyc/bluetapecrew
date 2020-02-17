const baseUrl = "https://localhost:44372"

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

  export async function  createCategory(category) {
    var result = await fetch(`${baseUrl}/categories`, 
    {
      method: 'POST',
      body: JSON.stringify(category),
      headers: {
        'Content-Type': 'application/json',
      }
    })
    return await result.json()
  }

  export async function  deleteCategory(category) {
    await fetch(`${baseUrl}/categories/${category.id}`, { method: 'DELETE' })
  }