import * as client from './apiClient'

const endpoint = "products"
export const get = async () => await client.get(endpoint)
export const create = async (product) => await client.create(endpoint, product)
export const update = async (product) => await client.update(endpoint, product)
export const del = async (id) => await client.del(endpoint, id)