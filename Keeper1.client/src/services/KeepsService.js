import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService{
    async getAll() {
        const res = await api.get('api/keeps')
        AppState.keeps = res.data
        logger.log('keeps', res.data)
    }

    async createKeep(body) {
        logger.log(body)
        const res = await api.post('api/keeps', body)
        logger.log(res.data)
        AppState.profileKeeps.unshift(res.data)
        
        
    }

    async getById(id) {
        const res = await api.get(`api/keeps/${id}`)
        logger.log(res.data)
    }
    async deleteKeep(id) {
        const res = await api.delete(`api/keeps/${id}`)
        logger.log(res.data)
        AppState.keeps = AppState.keeps.filter(k => k.id != id)
        
    }

}
export const keepsService = new KeepsService()