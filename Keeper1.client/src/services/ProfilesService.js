import { AppState } from "../AppState"
import { api } from "./AxiosService"

class ProfilesService{
    async getProfile(id) {
        const res = await api.get(`api/profiles/${id}`)
        AppState.profile = res.data
    }

    async getVaults(id) {
        const res = await api.get(`api/profiles/${id}/vaults`)
        AppState.profileVaults = res.data
    }

    async getKeeps(id) {
        const res = await api.get(`api/profiles/${id}/keeps`)
    }
}
export const profilesService = new ProfilesService()