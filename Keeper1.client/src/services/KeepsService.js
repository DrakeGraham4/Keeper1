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
        const res = await api.post('api/keeps', body)
        logger.log(res.data)
        AppState.keeps.push(res.data)
        return res.data
    }

}
export const keepsService = new KeepsService()