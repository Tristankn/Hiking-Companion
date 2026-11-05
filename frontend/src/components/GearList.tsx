import type { Gear } from '../types/Gear';

interface GearListProps {
    gearList: Gear[];
}

export function GearList({ gearList }: GearListProps){
    return (
        <ul>
            {gearList.map(gear => (
                <li key={gear.gearId}>{gear.name}</li>
            ))}
        </ul>

    )
}
