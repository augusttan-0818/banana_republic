"use client";
import React, { createContext, useState, useContext, ReactNode } from "react";
import { ProvinceOrTerritory } from "./ProvinceOrTerritory";

interface MenuContextType {
  year: string;
  codeBook: string;
  division: string;
  part: string;
  section: string;
  subSection: string;
  partsOptions: { partNumber: string; partString: string }[];
  sectionsOptions: { sectionNumber: string; sectionString: string }[];
  subSectionsOptions: { subSectionNumber: string; subSectionString: string }[];
  checkedStates: Record<ProvinceOrTerritory, boolean>;
  selectedRDRowIds: number[]
  selectAll: boolean;
  searchFlag: boolean;
  partError: boolean;
  topicYear: string;
  topic: string;
  topicOptions: string[];
  topicError: boolean;
  topicFlag: boolean;
  setYear: (year: string) => void;
  setCodeBook: (codeBook: string) => void;
  setDivision: (division: string) => void;
  setPart: (part: string) => void;
  setSection: (section: string) => void;
  setSubSection: (subSection: string) => void;
  setPartsOptions: (
    options: { partNumber: string; partString: string }[]
  ) => void;
  setSectionsOptions: (
    options: { sectionNumber: string; sectionString: string }[]
  ) => void;
  setSubSectionsOptions: (
    options: { subSectionNumber: string; subSectionString: string }[]
  ) => void;
  setCheckedStates: (
    checkedStates: Record<ProvinceOrTerritory, boolean>
  ) => void;
  setSelectAll: (selectAll: boolean) => void;
  setSearchFlag: (searchFlag: boolean) => void;
  setPartError: (partError: boolean) => void;
  setTopicYear: (topicYear: string) => void;
  setTopic: (topic: string) => void;
  setTopicOptions: (options: string[]) => void;
  setTopicFlag: (searchFlag: boolean) => void;
  setTopicError: (topicError: boolean) => void;
  setSelectedRDRowIds: (rowIds: number[]) => void;
}

export const MenuContext = createContext<MenuContextType | undefined>(
  undefined
);

export const MenuProvider = ({ children }: { children: ReactNode }) => {
  const [year, setYear] = useState("2020");
  const [codeBook, setCodeBook] = useState("NBC");
  const [division, setDivision] = useState("Div B");
  const [part, setPart] = useState("");
  const [section, setSection] = useState("");
  const [subSection, setSubSection] = useState("");
  const [partsOptions, setPartsOptions] = useState<
    { partNumber: string; partString: string }[]
  >([]);
  const [sectionsOptions, setSectionsOptions] = useState<
    { sectionNumber: string; sectionString: string }[]
  >([]);
  const [subSectionsOptions, setSubSectionsOptions] = useState<
    { subSectionNumber: string; subSectionString: string }[]
  >([]);
  const [searchFlag, setSearchFlag] = useState(false);
  const [partError, setPartError] = useState(false);
  const [topicYear, setTopicYear] = useState("2015");
  const [topic, setTopic] = useState("");
  const [topicOptions, setTopicOptions] = useState<string[]>([]);
  const [topicError, setTopicError] = useState(false);
  const [topicFlag, setTopicFlag] = useState(false);
  const [selectAll, setSelectAll] = useState(false);
  const [checkedStates, setCheckedStates] = useState<
    Record<ProvinceOrTerritory, boolean>
  >({
    all: false,
    ab: false,
    bc: false,
    mb: false,
    nb: false,
    nl: false,
    nt: false,
    ns: false,
    nu: false,
    on: false,
    pe: false,
    qc: false,
    sk: false,
    yt: false,
  });
  const [selectedRDRowIds, setSelectedRDRowIds] = useState<number[]>([]);

  return (
    <MenuContext.Provider
      value={{
        year,
        codeBook,
        division,
        part,
        section,
        subSection,
        partsOptions,
        sectionsOptions,
        subSectionsOptions,
        searchFlag,
        checkedStates,
        selectAll,
        partError,
        topic,
        topicYear,
        setYear,
        setCodeBook,
        setDivision,
        setPart,
        setSection,
        setSubSection,
        setPartsOptions,
        setSectionsOptions,
        setSubSectionsOptions,
        setCheckedStates,
        setSelectAll,
        setSearchFlag,
        setPartError,
        setTopicYear,
        setTopic,
        setTopicOptions,
        topicOptions,
        topicError,
        setTopicError,
        topicFlag,
        setTopicFlag,
        selectedRDRowIds,
        setSelectedRDRowIds
      }}>
      {children}
    </MenuContext.Provider>
  );
};

export const useMenu = (): MenuContextType => {
  const context = useContext(MenuContext);
  if (!context) {
    throw new Error("useMenuContext must be used within a MenuProvider");
  }
  return context;
};
