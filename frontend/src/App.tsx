import type { Gear } from './types/Gear.ts';
import { GearList } from './components/GearList.tsx'
import { getGearList } from './api/gearApi'
import './App.css'
import { useState } from 'react';

function App() {
  
  const [gearList, setGearList] = useState<Gear[]>([]);

  async function fetchGearList() {

    var res = await getGearList();

    setGearList(res);
    
  }

  
  return (

  <>
    
    <h1>HIKING COMPANION</h1>
    
    <button onClick={fetchGearList}>Show Equipment</button>



     <GearList gearList={gearList} />
     
  </>
  )
};

export default App
