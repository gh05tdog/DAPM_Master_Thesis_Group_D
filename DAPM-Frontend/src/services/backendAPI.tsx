import { Stream } from "stream";
import { json } from "stream/consumers";
import keycloak, { getToken } from "../utils/keycloak.ts"
import { environment } from "../configs/environments.ts";

const path = environment.clientapi_url;
const getStatusRetries = 100;
const getStatusDelay = 100;
const access = environment.accesscontrol_url;

export async function getAuthenticationHeader() {
    const options = {
        headers: {
            'Authorization': `Bearer ${await getToken()}`
        },
    };
    return options
}
export async function fetchDataFromTicketService(ticketId: string, errorMesssage: string): Promise<any> {
    const maxRetries = getStatusRetries;
    const delay = (ms: number) => new Promise(resolve => setTimeout(resolve, ms));

    for (let retries = 0; retries < maxRetries; retries++) {
        try {
            const response = await fetchResponse(ticketId);
            const data = await response.json();
            if (data.status) {
                return data;
            }
            await delay(getStatusDelay);
        } catch (error) {
            if (retries === maxRetries - 1) {
                throw new Error('Max retries reached');
            }
        }
    }
    throw new Error(errorMesssage);
};
export async function fetchResponse(ticket: string): Promise<any> {
    try {
        const response = await fetch(path + `/status/${ticket}`, await getAuthenticationHeader());
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        //console.log(jsonData)
        return response;
    } catch (error) {
        console.error('Error fetching status:', error);
        return error;
    }
}
export async function fetchOrganisations() {
    try {
        const response = await fetch(path + `/organizations`, await getAuthenticationHeader());;
        if (!response.ok) {
            throw new Error('Fetching orgs, Network response was not ok');
        }
        const jsonData = await response.json();
        return await fetchDataFromTicketService(jsonData.ticketId, 'Failed to fetch data');
    } catch (error) {
        console.error('Fetching orgs, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}
export async function fetchOrganisation(orgId: string) {
    try {
        const response = await fetch(path + `/Organizations/${orgId}`, await getAuthenticationHeader());
        if (!response.ok) {
            throw new Error('Fetching org, Network response was not ok');
        }
        const jsonData = await response.json();

        return await fetchDataFromTicketService(jsonData.ticketId, 'Failed to fetch data');
    } catch (error) {
        console.error('Fetching org, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}

export async function fetchOrganisationRepositories(orgId: string) {
    try {
        const response = await fetch(path + `/Organizations/${orgId}/repositories`, await getAuthenticationHeader());
        if (!response.ok) {
            throw new Error('Fecthing reps, Network response was not ok');
        }
        const jsonData = await response.json();

        return await fetchDataFromTicketService(jsonData.ticketId, 'Failed to fetch data');
    } catch (error) {
        console.error('Fecthing reps, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}
export async function fetchRepository(orgId: string, repId: string) {
    try {
        const response = await fetch(path + `/Organizations/${orgId}/repositories/${repId}`, await getAuthenticationHeader());
        if (!response.ok) {
            throw new Error('Fecthing rep, Network response was not ok');
        }
        const jsonData = await response.json();

        return await fetchDataFromTicketService(jsonData.ticketId, 'Failed to fetch data');
    } catch (error) {
        console.error('Fecthing rep, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}
export async function fetchRepositoryResources(orgId: string, repId: string) {
    try {
        const response = await fetch(path + `/Organizations/${orgId}/repositories/${repId}/resources`, await getAuthenticationHeader());
        if (!response.ok) {
            throw new Error('Fetching resources, Network response was not ok');
        }
        const jsonData = await response.json();
        return await fetchDataFromTicketService(jsonData.ticketId, 'Failed to fetch data');
    } catch (error) {
        console.error('Fetching resources, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}
export async function fetchResource(orgId: string, repId: string, resId: string) {
    try {
        const response = await fetch(path + `/Organizations/${orgId}/repositories/${repId}/resources/${resId}`, await getAuthenticationHeader());
        if (!response.ok) {
            throw new Error('Fetching resource, Feching Network response was not ok');
        }
        const jsonData = await response.json();
        return await fetchDataFromTicketService(jsonData.ticketId, 'Failed to fetch data');
    } catch (error) {
        console.error('Fetching resource, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}

export async function fetchRepositoryPipelines(orgId: string, repId: string) {
    try {
        const response = await fetch(path + `/Organizations/${orgId}/repositories/${repId}/pipelines`, await getAuthenticationHeader());
        if (!response.ok) {
            throw new Error('fetching pipelines, Network response was not ok');
        }
        const jsonData = await response.json();
        return await fetchDataFromTicketService(jsonData.ticketId, 'Failed to fetch data');
    } catch (error) {
        console.error('fetching pipelines, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}
export async function fetchPipeline(orgId: string, repId: string, pipId: string) {
    try {
        const response = await fetch(path + `/Organizations/${orgId}/repositories/${repId}/pipelines/${pipId}`, await getAuthenticationHeader());
        if (!response.ok) {
            throw new Error('fetching pipeline, Network response was not ok');
        }
        const jsonData = await response.json();
        return await fetchDataFromTicketService(jsonData.ticketId, 'Failed to fetch data');
    } catch (error) {
        console.error('fetching pipeline, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}
export async function putRepository(orgId: string, repositoryName: string) {

    const headers = new Headers()
    headers.append("accept", "application/json")
    headers.append("Content-Type", "application/json")
    headers.append("Authorization", `Bearer ${await getToken()}`)


    try {
        const response = await fetch(path + `/Organizations/${orgId}/repositories`, {
            method: "POST",
            headers: headers,
            body: JSON.stringify({ name: repositoryName })
        });

        if (!response.ok) {
            throw new Error('put rep, Network response was not ok');
        }

        const jsonData = await response.json();
        return await fetchDataFromTicketService(jsonData.ticketId, 'Failed to post repository');
    } catch (error) {
        console.error('put rep, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}

export async function putResource(orgId: string, repId: string, formData: FormData) {
    try {
        const response = await fetch(path + `/Organizations/${orgId}/repositories/${repId}/resources`, {
            method: "POST",
            body: formData,
            headers: {
                'Authorization': `Bearer ${await getToken()}`
            },
        });

        if (!response.ok) {
            throw new Error('put res, Network response was not ok');
        }

        const jsonData = await response.json();

        return await fetchDataFromTicketService(jsonData.ticketId, 'Failed to fetch data');
    } catch (error) {
        console.error('put res, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}

export async function fetchPipelineUsers() {
    try {
      const response = await fetch(access + `/api/access-control/pipeline/get-all-user-pipelines/`, {
          headers: {
              'Authorization': `Bearer ${await getToken()}`
          },
      });
      if (!response.ok) {
          throw new Error('Network response was not ok');
      }
      const jsonData = await response.json();
      //console.log(jsonData)
      return jsonData;
    } catch (error) {
          console.error('Error fetching status:', error);
          return error;
    }
  } 

  export async function removeUserPipeline(userId: string, pipelineId: string) {
    try {
        
        const response = await fetch(access + `/api/access-control/pipeline/remove-user-pipeline/`, {
            method: "POST",
            headers: {
                'Authorization': `Bearer ${await getToken()}`,
                'content-type': 'application/json'
            },
            body: JSON.stringify({userId: userId, pipelineId: pipelineId})
        });
      if (!response.ok) {
          throw new Error('Network response was not ok');
      }
      const jsonData = await response.json();
      return jsonData;
    } catch (error) {
          console.error('Error fetching status:', error);
          return error;
    }
  } 

  export async function addUserPipeline(userId: string, pipelineId: string) {
    try {
        
        const response = await fetch(access + `/api/access-control/pipeline/add-user-pipeline/`, {
            method: "POST",
            headers: {
                'Authorization': `Bearer ${await getToken()}`,
                'content-type': 'application/json'
            },
            body: JSON.stringify({userId: userId, pipelineId: pipelineId})
        });
      if (!response.ok) {
          throw new Error('Network response was not ok');
      }
      const jsonData = await response.json();
      return jsonData;
    } catch (error) {
          console.error('Error fetching status:', error);
          return error;
    }
  }

export async function fetchResourceUsers() {
    try {
        const response = await fetch(access + `/api/access-control/resource/get-all-user-resources/`, {
            headers: {
                'Authorization': `Bearer ${await getToken()}`
            },
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const jsonData = await response.json();
        //console.log(jsonData)
        return jsonData;
    } catch (error) {
        console.error('Error fetching status:', error);
        return error;
    }
}

export async function removeUserResource(userId: string, resourceId: string) {
    try {

        const response = await fetch(access + `/api/access-control/resource/remove-user-resource/`, {
            method: "POST",
            headers: {
                'Authorization': `Bearer ${await getToken()}`,
                'content-type': 'application/json'
            },
            body: JSON.stringify({userId: userId, resourceId: resourceId})
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const jsonData = await response.json();
        return jsonData;
    } catch (error) {
        console.error('Error fetching status:', error);
        return error;
    }
}

export async function addUserResource(userId: string, resourceId: string) {
    try {

        const response = await fetch(access + `/api/access-control/resource/add-user-resource/`, {
            method: "POST",
            headers: {
                'Authorization': `Bearer ${await getToken()}`,
                'content-type': 'application/json'
            },
            body: JSON.stringify({userId: userId, resourceId: resourceId})
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const jsonData = await response.json();
        return jsonData;
    } catch (error) {
        console.error('Error fetching status:', error);
        return error;
    }
}

export async function fetchRepositoryUsers() {
    try {
        const response = await fetch(access + `/api/access-control/repository/get-all-user-repositories/`, {
            headers: {
                'Authorization': `Bearer ${await getToken()}`
            },
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const jsonData = await response.json();
        //console.log(jsonData)
        return jsonData;
    } catch (error) {
        console.error('Error fetching status:', error);
        return error;
    }
}

export async function removeUserRepository(userId: string, repositoryId: string) {
    try {

        const response = await fetch(access + `/api/access-control/pipeline/remove-user-repository/`, {
            method: "POST",
            headers: {
                'Authorization': `Bearer ${await getToken()}`,
                'content-type': 'application/json'
            },
            body: JSON.stringify({userId: userId, repositoryId: repositoryId})
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const jsonData = await response.json();
        return jsonData;
    } catch (error) {
        console.error('Error fetching status:', error);
        return error;
    }
}

export async function addUserRepository(userId: string, repositoryId: string) {
    try {

        const response = await fetch(access + `/api/access-control/pipeline/add-user-repository/`, {
            method: "POST",
            headers: {
                'Authorization': `Bearer ${await getToken()}`,
                'content-type': 'application/json'
            },
            body: JSON.stringify({userId: userId, repositoryId: repositoryId})
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const jsonData = await response.json();
        return jsonData;
    } catch (error) {
        console.error('Error fetching status:', error);
        return error;
    }
}



export async function putPipeline(orgId: string, repId: string, pipelineData:any){
    console.log(pipelineData)
    try {
        const response = await fetch(`${path}/Organizations/${orgId}/repositories/${repId}/pipelines`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
                'Authorization': `Bearer ${await getToken()}`
            },
            body: JSON.stringify(pipelineData)
        });

        if (!response.ok) {
            throw new Error('put pipeline, Network response was not ok');
        }

        const jsonData = await response.json();
        const ticketData = await (fetchDataFromTicketService(jsonData.ticketId, 'Failed to put pipeline'));
        return ticketData.result.itemIds.pipelineId as string;
    } catch (error) {
        console.error('put pipeline, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}
export async function putExecution(orgId: string, repId: string, pipeId: string) {
    try {
        const response = await fetch(`${path}/Organizations/${orgId}/repositories/${repId}/pipelines/${pipeId}/executions`, {
            method: "POST",
            headers: {
                'Authorization': `Bearer ${await getToken()}`
            },
        });

        if (!response.ok) {
            throw new Error('put execution, Network response was not ok');
        }

        const jsonData = await response.json();
        const ticketData = await fetchDataFromTicketService(jsonData.ticketId, 'Failed to post execution');
        return ticketData.result.itemIds.executionId as string;

    } catch (error) {
        console.error('put execution, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}
export async function putCommandStart(orgId: string, repId: string, pipeId: string, exeId: string) {
    try {
        const response = await fetch(`${path}/Organizations/${orgId}/repositories/${repId}/pipelines/${pipeId}/executions/${exeId}/commands/start`, {
            method: "POST",
            headers: {
                'Authorization': `Bearer ${await getToken()}`
            },
        });

        if (!response.ok) {
            throw new Error('put command start, Network response was not ok');
        }

        const jsonData = await response.json();
        return await fetchDataFromTicketService(jsonData.ticketId, 'Failed to command start');
    } catch (error) {
        console.error('put command start, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}
export async function putOperator(orgId: string, repId: string, formData: FormData) {
    try {
        const response = await fetch(path + `/Organizations/${orgId}/repositories/${repId}/resources/operators`, {
            method: "POST",
            body: formData,
            headers: {
                'Authorization': `Bearer ${await getToken()}`
            },
        });

        if (!response.ok) {
            throw new Error('put res, Network response was not ok');
        }

        const jsonData = await response.json();
        return await fetchDataFromTicketService(jsonData.ticketId, 'Failed to fetch data');
    } catch (error) {
        console.error('put res, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}
export async function PostNewPeer(domainName: string) {
    try {
        const formData = new FormData();
        formData.append('targetPeerDomain', domainName);

        const headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append("Authorization", `Bearer ${await getToken()}`)

        const response = await fetch(path + `/system/collab-handshake`, {
            method: "POST",
            body: JSON.stringify({ targetPeerDomain: domainName }),
            headers: headers
        });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const jsonData = await response.json();
        return await fetchDataFromTicketService(jsonData.ticketId, 'Failed to post new peer');
    } catch (error) {
        console.error('post new peer, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}
export async function downloadResource(organizationId: string, repositoryId: string, resourceId: string) {
    try {
        const response = await fetch(path + `/organizations/${organizationId}/repositories/${repositoryId}/resources/${resourceId}/file`, {
            headers: {
                'Authorization': `Bearer ${await getToken()}`
            },
        });
        if (!response.ok) {
            throw new Error('Fetching orgs, Network response was not ok');
        }
        const jsonData = await response.json();

        // Fetch additional data recursively
        const getData = async (ticketId: string): Promise<any> => {
            const maxRetries = getStatusRetries;
            const delay = (ms: number) => new Promise(resolve => setTimeout(resolve, ms));

            for (let retries = 0; retries < maxRetries; retries++) {
                try {
                    const response = await fetchResponse(ticketId) as any;
                    console.log(response)
                    if (response.ok) {
                        await delay(getStatusDelay);
                        return response;
                    }
                    await delay(getStatusDelay);
                } catch (error) {
                    if (retries === maxRetries - 1) {
                        throw new Error('Max retries reached');
                    }
                }
            }
            throw new Error('Failed to fetch data');
        };

        // Call getData function with the ticketId obtained from fetchOrganisations
        return await getData(jsonData.ticketId);
    } catch (error) {
        console.error('Fetching orgs, Error fetching data:', error);
        throw error; // Propagate error to the caller
    }
}
