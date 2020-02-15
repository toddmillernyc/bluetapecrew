import React, { useState } from 'react';
import Switch from 'react-switch'

function CategoryNew() {

    const [published, setPublished] = useState(false)
    const [name, setName] = useState("")
    
    return <tr className="row">
                <td className="col">
                    <input 
                        value={name}
                        onChange={() => setName(name) }></input>
                </td>
                <td className="col">
                    <Switch
                        height={21}
                        width={42}
                        checked={published}
                        onChange={() => setPublished(!published)} />
                </td>
                <td className="col"></td>
            </tr>

}

export default CategoryNew