export interface IProject{
    name: string;
}

export class Project implements IProject{
    name: string;

    constructor(name:string){
        this.name = name;
    }
}