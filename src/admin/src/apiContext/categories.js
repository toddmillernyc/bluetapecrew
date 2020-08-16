import * as client from './apiClient'

const endpoint = "categories"
export const get = async () => await client.get(endpoint)
export const create = async (category) => await client.create(endpoint, category)
export const update = async (category) => await client.update(endpoint, category)
export const del = async (id) => await client.del(endpoint, id)