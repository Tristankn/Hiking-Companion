import type { Gear } from '../types/Gear';

export async function getGearList(): Promise<Gear[]> {
    const url = "http://localhost:5289/gear";

    
    const response = await fetch(url);
    if(!response.ok) {
        throw new Error ('Response Status: ${response.status}');
    }

    const result = await response.json();
    
    return result;


    
}