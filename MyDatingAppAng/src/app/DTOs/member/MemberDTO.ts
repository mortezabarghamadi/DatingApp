import { PhotoDTO } from "../photo/PhotoDTO";

export interface MemberDTO {
    id: number;
    userName: string;
    photoUrl: string;
    age: number;
    knownAs: string;
    created: Date;
    lastActive: Date;
    gender: string;
    introduction: string;
    lookingFor: string;
    intrests: string;
    city: string;
    country: string;
    photos: PhotoDTO[];
}