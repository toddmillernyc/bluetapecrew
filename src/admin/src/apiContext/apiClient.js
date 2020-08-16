
const baseUrl = "https://api.toddmiller.nyc"

export async function get(endpoint) {
    let response = await fetch(`${baseUrl}/${endpoint}`)
    return await response.json()
}

export async function update(endpoint, payload) {
    await fetch(`${baseUrl}/${endpoint}`, 
    {
      method: 'PUT',
      body: JSON.stringify(payload),
      headers: {
        'Content-Type': 'application/json',
      }
    })
  }

  export async function create(endpoint, payload) {
    var result = await fetch(`${baseUrl}/${endpoint}`, 
    {
      method: 'POST',
      body: JSON.stringify(payload),
      headers: {
        'Content-Type': 'application/json',
      }
    })
    return await result.json()
  }

  export async function del(endpoint, id) {
    const response = await fetch(`${baseUrl}/${endpoint}/${id}`, { method: 'DELETE' })
    if(response.status === 200) return
    return JSON.stringify(await response.text())
  }