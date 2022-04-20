import { AppState } from "../AppState"
import { api } from "./AxiosService"

class VaultsService{

    async createVault(body) {
        const res = await api.post('api/vaults', body)
        AppState.profileVaults.unshift(res.data)
        
    }

}

export const vaultsService = new VaultsService()