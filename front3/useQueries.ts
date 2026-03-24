import { useQuery } from "@tanstack/react-query";
import { searchCcrs } from "../utils/api";
import { ccrSearch } from "../types/schema";

const useSearchCCRs = (form: ccrSearch, page: number, pageSize: number,enabled: boolean) => {
  return useQuery({
    queryKey: ["ccr", form, page, pageSize],
    queryFn: () => searchCcrs(form, page, pageSize),
    enabled: enabled
  });
};

export { useSearchCCRs };
